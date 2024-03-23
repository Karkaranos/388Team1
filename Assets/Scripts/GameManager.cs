using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PlayerLives = 3;

    public int MaxHitCounter = 10;
    [HideInInspector]public int currentHitCounter = 0;

    public void LoseLife()
    {
        PlayerLives--;
        if (PlayerLives <= 0)
        {
            LoseGame();
        }
    }

    public void HitBall()
    {
        currentHitCounter++;
        if (currentHitCounter >= MaxHitCounter)
        {
            LoseGame();
        }
    }

    public void LoseGame()
    {
        //handle losing the game
    }
}
