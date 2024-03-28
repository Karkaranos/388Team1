using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerupMenuManager : MonoBehaviour
{
    [System.Serializable]
    public class powerUpIcon
    {
        public Sprite icon;
        public GameManager.LastingPowerupType type;
        public TMP_Text text;

        public powerUpIcon(Sprite icon, GameManager.LastingPowerupType type, TMP_Text text)
        {
            this.icon = icon;
            this.type = type;
            this.text = text;
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

    [SerializeField] private PlayerInput actionMap;
    private InputAction back;

    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        actionMap.currentActionMap.Enable();
        back = actionMap.currentActionMap.FindAction("Back");
        back.started += Back_started;

        defaultPowerupIcon = powerupDescriptionIcon.sprite;
        HoveringOverPowerup(0);
        UpdatePowerups();
    }

    private void Back_started(InputAction.CallbackContext obj)
    {
        Back();
    }

    private void UpdatePowerups()
    {
        for (int i = 0; i < powerupIcons.Length; i++)
        {
            //check to see if you have the powerup first
            if (gm.unusedLingeringPowerups.Contains(GameManager.LastingPowerupType.Comet))
            {
                powerups[i].GetComponent<Image>().sprite = powerupIcons[i].icon;
                int count = 0;
                foreach (GameManager.LastingPowerupType com in gm.unusedLingeringPowerups)
                {
                    if (com.Equals(GameManager.LastingPowerupType.Comet))
                    {
                        count++;
                    }
                }
                powerupIcons[i].text.text = "x" + count;
            }
            else
            {
                powerups[i].GetComponent<Image>().sprite = greyscaleIcons[i].icon;
                greyscaleIcons[i].text.text = "x0";
            }
        }
    }

    public void UnitSelected(int player)
    {
        currentSelectedPlayer = player;
        players[player].GetComponent<Image>().color = Color.red;
        //grab the powerup of the corresponding player
        //currently just sets to none
        GameManager.LastingPowerupType powerup = FindObjectOfType<GameManager>().UsedPowerups[player];
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
            default:
                Debug.Log("Type is outside of enum range");
                break;
        }
    }

    public void SelectedPowerup(int KindOfPowerup)
    {
        GameManager.LastingPowerupType type = (GameManager.LastingPowerupType)KindOfPowerup;

        if (currentSelectedPlayer >= 0 && currentSelectedPlayer <= 4)
        {
            gm.GivePowerup(currentSelectedPlayer, type);
            HoveringOverPowerup(0);
            UpdatePowerups();
            Back();
        }
        
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
                currentSelectedPlayer = -1;
            }
        }
    }

    public void ClearPowerups()
    { 
        gm.ClearPowerups();
        UpdatePowerups();
    }

    public void NextLevel()
    {
        gm.LeavePowerupMenu();
    }
}
