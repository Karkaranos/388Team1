using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public bool Kickable;
    public GameObject KickBox;
    public GameManager.LastingPowerupType currentPowerup;
    public void kick()
    {
        if(Kickable)
        {
            KickBox.GetComponent<KickBox>().Kickball();
        }
    }
}
