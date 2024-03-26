using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerupMenuManager : MonoBehaviour
{
    [System.Serializable]
    public class powerUpIcon
    {
        public Sprite icon;
        public GameManager.LastingPowerupType type;

        public powerUpIcon(Sprite icon, GameManager.LastingPowerupType type)
        {
            this.icon = icon;
            this.type = type;
        }
    }

    [SerializeField] private bool hasComet;

    [SerializeField] TMP_Text unitText;
    [SerializeField] TMP_Text powerupDescriptionText;

    [SerializeField] private Image powerupDescriptionIcon;
    private Sprite defaultPowerupIcon;

    [SerializeField] private Image playerDescriptionIcon;


    [SerializeField] private int currentSelectedPlayer;

    [SerializeField] GameObject[] players;
    [SerializeField] GameObject[] powerups;

    [SerializeField] powerUpIcon[] powerupIcons;
    [SerializeField] private powerUpIcon[] greyscaleIcons;
    // Start is called before the first frame update
    void Start()
    {
        defaultPowerupIcon = powerupDescriptionIcon.sprite;
        HoveringOverPowerup(0);
        UpdatePowerups();
    }


    private void UpdatePowerups()
    {
        for (int i = 0; i < powerupIcons.Length; i++)
        {
            //check to see if you have the powerup first
            if (hasComet)
            {
                powerups[i].GetComponent<Image>().sprite = powerupIcons[i].icon;
            }
            else
            {
                powerups[i].GetComponent<Image>().sprite = greyscaleIcons[i].icon;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitSelected(int player)
    {
        currentSelectedPlayer = player;
        players[player].GetComponent<Image>().color = Color.red;
        //grab the powerup of the corresponding player
        //currently just sets to none
        GameManager.LastingPowerupType powerup = GameManager.LastingPowerupType.None;
        switch (powerup)
        {
            case GameManager.LastingPowerupType.None:
                unitText.text = "No Powerup Currently Held";
                break;
            case GameManager.LastingPowerupType.Comet:
                unitText.text = "Comet Powerup: Destroys bricks in an explosion around it when" +
                    "it hits a brick.";
                break;
        }

        playerDescriptionIcon.color = Color.red;

        EventSystem.current.SetSelectedGameObject(powerups[0]);
    }

    public void HoveringOverPowerup(int type)
    {
        switch ((GameManager.LastingPowerupType)type)
        {
            case GameManager.LastingPowerupType.None:
                powerupDescriptionText.text = "No Powerup Selected";
                powerupDescriptionIcon.sprite = defaultPowerupIcon;
                break;
            case GameManager.LastingPowerupType.Comet:
                powerupDescriptionText.text = "Comet Powerup: Destroys bricks in an explosion around it when" +
                    "it hits a brick.";
                for (int i = 0; i < powerupIcons.Length; i++)
                {
                    if (powerupIcons[i].type == GameManager.LastingPowerupType.Comet)
                    {
                        powerupDescriptionIcon.sprite = powerupIcons[i].icon;
                    }
                }
                break;
        }
    }

    public void SelectedPowerup(GameManager.LastingPowerupType type)
    {
        //set the currently selected player's current powerup to type
        Back();
    }

    public void Back()
    {
        foreach (GameObject go in powerups)
        {
            if (EventSystem.current.currentSelectedGameObject.Equals(go))
            {
                foreach(GameObject player in players)
                {
                    player.GetComponent<Image>().color = Color.white;
                    playerDescriptionIcon.color = Color.white;
                }
                EventSystem.current.SetSelectedGameObject(players[0]);

            }
        }
    }

    public void ClearPowerups()
    {
        Debug.Log("Clearing all powerups");
        //clear powerups code here
    }

    public void NextLevel()
    {
        Debug.Log("Proceeding to next level");
        //next level code here
    }
}
