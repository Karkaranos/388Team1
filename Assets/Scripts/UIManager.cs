using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject LivesText;
    public GameObject HitsText;
    public GameObject PowerupText;
    public bool Hits;
    // Start is called before the first frame update
    void Start()
    {
        Hits = !GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().LoseConditionIsPlayerLivesIfTrue;
        if (Hits)
        {
            LivesText.SetActive(false);
        }
        else
        {
            HitsText.SetActive(false);
        }
        hidePowerup();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().updateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLives(int Lives)
    {
        LivesText.GetComponent<TMP_Text>().text = "Lives: " + Lives;
    }

    public void updateHits(int Hits)
    {
        HitsText.GetComponent<TMP_Text>().text = "Hits: " + Hits;
    }

    public void powerupObtained(string Power)
    {
        PowerupText.SetActive(true);
        PowerupText.GetComponent<TMP_Text>().text = Power + " Obtained!";
        Invoke("hidePowerup", 2);
    }

    private void hidePowerup()
    {
        PowerupText.SetActive(false);
    }
}
