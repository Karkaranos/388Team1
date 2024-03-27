using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Profiling.Editor;
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

    [SerializeField, Tooltip("This is where you choose how big the comet's explosion is")]
    private float cometExplosionRadius;

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
