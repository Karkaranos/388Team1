using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool LoseConditionIsPlayerLivesIfTrue = false;
    public int PlayerLives = 3;

    public int MaxHitCounter = 10;
    [HideInInspector] public int currentHitCounter = 0;

    [SerializeField] private List<bool> levelsCompleted;

    public bool ResetLivesAfterLevel;

    public bool hasBeenToPowerupMenu;

    [HideInInspector] public enum LastingPowerupType
    {
        None,
        Comet
    }

    [HideInInspector] public enum LimitedPowerupType
    {
        None,
        BiggerBall,
        ExtraLife
    }

    public List<LastingPowerupType> unusedLingeringPowerups = new List<LastingPowerupType>();
    public LastingPowerupType[] UsedPowerups = new LastingPowerupType[5];


    private void Awake()
    {
        //magic number is the number of non level scenes in the build
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 4; i++)
        {
            levelsCompleted.Add(false);
        }
        GameObject[] managers = GameObject.FindGameObjectsWithTag("GameController");
        if(managers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /*public void LoseLife()
    {
        if (LoseConditionIsPlayerLivesIfTrue)
        {
            PlayerLives--;
            GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().updateLives(PlayerLives);
            if (PlayerLives <= 0)
            {
                LoseGame();
            }
        }
    }*/
    public void HitBall()
    {

        /*if (!LoseConditionIsPlayerLivesIfTrue)
        {*/
            if (MaxHitCounter <= 0)
            {
                Debug.Log("Max Hit Counter Cannot be less than zero");
            }
            else
            {
                currentHitCounter++;
                GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().updateHits(MaxHitCounter - currentHitCounter);
                if (currentHitCounter >= MaxHitCounter)
                {
                    LoseGame();
                }
            }   
       /* }*/
    }

    public void BeatLevel()
    {
        bool wonGame = false;
        for (int i = 0; i < levelsCompleted.Count; i++)
        {

            if (levelsCompleted[i] == false)
            {
                if (i >= levelsCompleted.Count - 1)
                {
                    wonGame = true;
                    break;
                }

                levelsCompleted[i] = true;
                break;
            }
        }
        if (wonGame)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
       
    }
    public void LoseGame()
    {
        SceneManager.LoadScene(3);
    }

    public void LeavePowerupMenu()
    {
        if (ResetLivesAfterLevel)
        {
            currentHitCounter = 0;
        }
        int index = 4;
        for (int i = 0; i < levelsCompleted.Count; i++)
        {
            if (levelsCompleted[i] == false)
            {
                index += i;
                break;
            }
        }
        SceneManager.LoadScene(index);
    }

    public void ObtainedPowerup(LastingPowerupType type)
    {
        if (type != LastingPowerupType.None)
        {
            //the lasting use powerups
            if (type == LastingPowerupType.Comet)
            {
                GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().powerupObtained("Comet");
                unusedLingeringPowerups.Add(type);
                unusedLingeringPowerups.Sort();
            }
        }
    }

    public void ObtainedPowerup(LimitedPowerupType type)
    {
        if (type != LimitedPowerupType.None)
        {
            switch (type)
            {
                case LimitedPowerupType.ExtraLife:
                    PlayerLives++;
                    currentHitCounter -= 2;
                    updateText();
                    GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().powerupObtained("Extra Life");
                    break;
                case LimitedPowerupType.BiggerBall:
                    BallBehavior bBehav = FindObjectOfType<BallBehavior>();
                    GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().powerupObtained("Bigger Ball");
                    bBehav.BigBallInventory++;
                    if (!bBehav.isBig)
                    {
                        bBehav.StartCoroutine(bBehav.BigBall());
                    }
                    break;
            }
        }
    }
    

    public void GivePowerup(int index, LastingPowerupType Powerup)
    {
        bool available = false;
        foreach(LastingPowerupType power in unusedLingeringPowerups)
        {
            if(power == Powerup)
            {
                available= true;
            }
        }

        if (available)
        {
            if (UsedPowerups[index] != LastingPowerupType.None)
            {
                unusedLingeringPowerups.Add(UsedPowerups[index]);
            }

            UsedPowerups[index] = Powerup;
            unusedLingeringPowerups.Remove(Powerup);
        }
        
    }

    public void ClearPowerups()
    {
        for(int i = 0; i < UsedPowerups.Length; i++)
        {
            LastingPowerupType type = UsedPowerups[i];
            if (type != LastingPowerupType.None)
            {
                unusedLingeringPowerups.Add(type);
            }
            UsedPowerups[i] = LastingPowerupType.None;
        }
    }

    public void givePlayersPowerups()
    {
        GameObject.Find("Player 1").GetComponent<Players>().currentPowerup = UsedPowerups[0];
        GameObject.Find("Player 2").GetComponent<Players>().currentPowerup = UsedPowerups[1];
        GameObject.Find("Player 3").GetComponent<Players>().currentPowerup = UsedPowerups[2];
        GameObject.Find("Player 4").GetComponent<Players>().currentPowerup = UsedPowerups[3];
        GameObject.Find("Player 5").GetComponent<Players>().currentPowerup = UsedPowerups[4];
    }

    public void updateText()
    {
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().updateHits(MaxHitCounter - currentHitCounter);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().updateLives(PlayerLives);
    }


    public void RestartGame()
    {
        ClearPowerups();
        unusedLingeringPowerups.Clear();
        PlayerLives = 3;
        currentHitCounter = 0;
        for (int i = 0; i < levelsCompleted.Count; i++)
        {
            levelsCompleted[i] = false;
        }
        hasBeenToPowerupMenu = false;
    }
}
