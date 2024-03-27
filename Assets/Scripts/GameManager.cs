using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool LoseConditionIsPlayerLivesIfTrue = true;
    public int PlayerLives = 3;

    public int MaxHitCounter = 10;
    [HideInInspector] public int currentHitCounter = 0;



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

    public List<LastingPowerupType> obtainedLingeringPowerups = new List<LastingPowerupType>();
    public List<LastingPowerupType> unusedLingeringPowerups = new List<LastingPowerupType>();
    public LastingPowerupType[] UsedPowerups = new LastingPowerupType[5];


    private void awake()
    {
        GameObject[] managers = GameObject.FindGameObjectsWithTag("GameController");
        if(managers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoseLife()
    {
        if (LoseConditionIsPlayerLivesIfTrue)
        {
            PlayerLives--;
            if (PlayerLives <= 0)
            {
                LoseGame();
            }
        }
    }
    public void HitBall()
    {

        if (!LoseConditionIsPlayerLivesIfTrue)
        {
            if (MaxHitCounter <= 0)
            {
                Debug.Log("Max Hit Counter Cannot be less than zero");
            }
            else
            {
                currentHitCounter++;
                if (currentHitCounter >= MaxHitCounter)
                {
                    LoseGame();
                }
            }   
        }
    }

    public void ObtainedPowerup(LastingPowerupType type)
    {
        if (type != LastingPowerupType.None)
        {
            //the limited use powerups
            if (type == LastingPowerupType.Comet)
            {
                obtainedLingeringPowerups.Add(type);
                obtainedLingeringPowerups.Sort();
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
                    break;
                case LimitedPowerupType.BiggerBall:
                    BallBehavior bBehav = FindObjectOfType<BallBehavior>();
                    bBehav.BigBallInventory++;
                    if (!bBehav.isBig)
                    {
                        bBehav.StartCoroutine(bBehav.BigBall());
                    }
                    break;
            }
        }
    }
    public void LoseGame()
    {
        //handle losing the game
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
            UsedPowerups[index] = Powerup;
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
}
