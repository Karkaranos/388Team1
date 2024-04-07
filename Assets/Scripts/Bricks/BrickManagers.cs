using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using Unity.Profiling.Editor;
using UnityEngine;

public class BrickManagers : MonoBehaviour
{
    [Header("Bricks"), Tooltip("put the bricks in here"), SerializeField]
    public List<GameObject> bricks = new List<GameObject>();
    
    [Tooltip("This is the number of key bricks in this level"), SerializeField]
    private int keys = 0;

    [Header("Debug"), Tooltip("number of key bricks defeated"), SerializeField]
    private int keysDestroyed;
    [SerializeField, Tooltip("used for checking how many bricks are spawned")] private int bricksSpawned;
    [SerializeField, Tooltip("list of key bricks")] private List<GameObject> keyBrickList;


    [SerializeField, Header("Powerups for the level"), Tooltip("Enter the lasting powerups for the level here")]
    private List<GameManager.LastingPowerupType> lastingPowerups;
    [SerializeField, Tooltip("Enter the limited powerups for the level")]
    private List<GameManager.LimitedPowerupType> limitedPowerups;

    // Start is called before the first frame update
    void Start()
    {
        StartLevel();   
    }

    public void StartLevel()
    {
        SetKeyBricks(0);

        foreach(GameObject go in bricks)
        {
            BrickBehavior bbehav = go.GetComponent<BrickBehavior>();
            bbehav.MakeBrick();
        }

        FindObjectOfType<GameManager>().givePlayersPowerups();
    }

    public void SetKeyBricks(int count)
    {
        foreach (GameObject keyBrick in bricks)
        {
            BrickBehavior brickBehavior = keyBrick.GetComponent<BrickBehavior>();
            if (Random.value >= 0.7f && !brickBehavior.isKey)
            {
                brickBehavior.isKey = true;
                keyBrickList.Add(keyBrick);
                count++;
            }
            if (count >= keys)
            {
                break;
            }
        }
        if (count < keys)
        {
            SetKeyBricks(count);
        }
        
        foreach (GameManager.LastingPowerupType type in lastingPowerups)
        {
            bool found = false;
            while (!found)
            {
                BrickBehavior behav = keyBrickList[Random.Range(0,
                                keyBrickList.Count)].GetComponent<BrickBehavior>();

                if (behav.heldLastingPowerup == GameManager.LastingPowerupType.None)
                {
                    behav.heldLastingPowerup = type;
                    found = true;
                }
            }
        }
        foreach (GameManager.LimitedPowerupType type in limitedPowerups)
        {
            bool found = false;
            while (!found)
            {
                BrickBehavior behav = bricks[Random.Range(0,
                                bricks.Count)].GetComponent<BrickBehavior>();

                if (behav.heldLimitedPowerup == GameManager.LimitedPowerupType.None &&
                    behav.isKey == false)
                {
                    behav.heldLimitedPowerup = type;
                    found = true;
                }
            }
        }
    }


    

    public void keyDestroyed(GameObject key)
    {
        if(key.GetComponent<BrickBehavior>().isKey)
        {
            keysDestroyed++;
            if (keysDestroyed >= keys)
            {
                FindObjectOfType<GameManager>().BeatLevel();
            }
        }
        Destroy(key);
    }

    
}
