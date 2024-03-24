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
    [HideInInspector] public enum PowerupType
    {
        None,
        Comet,
        ExtraLife,
        BiggerBall
    }
    [SerializeField] private List<PowerupType> obtainedPowerups = new List<PowerupType>();

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

    public void ObtainedPowerup(PowerupType type)
    {
        if (type != PowerupType.None)
        {
            //the limited use powerups
            if (type == PowerupType.Comet)
            {
                obtainedPowerups.Add(type);
                obtainedPowerups.Sort();
            }
            switch (type)
            {
                case PowerupType.ExtraLife:
                    PlayerLives++;
                    break;
                case PowerupType.BiggerBall:
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
}
