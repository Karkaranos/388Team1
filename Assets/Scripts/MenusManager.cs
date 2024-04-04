using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenusManager : MonoBehaviour
{
    public GameObject CreditsScreen;
    public PlayerInput ActionMap;
    public InputAction Back;
    public GameObject StartButton;
    public GameObject BackButton;

    private void Awake()
    {
        ActionMap.currentActionMap.Enable();
        Back = ActionMap.currentActionMap.FindAction("Back");
        Back.started += Back_started;
    }

    private void Back_started(InputAction.CallbackContext obj)
    {
        if (CreditsScreen.activeInHierarchy == true)
        {
            LeaveCredits();
        }
    }

    public void StartGame()
    {
        if(GameObject.FindGameObjectWithTag("GameController") != null)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().RestartGame();
        }
        SceneManager.LoadScene(4);
    }

    public void Credits()
    {
        CreditsScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(BackButton);
    }
    
    public void LeaveCredits()
    {
        EventSystem.current.SetSelectedGameObject(StartButton);
        CreditsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        FindObjectOfType<GameManager>().RestartGame();
        SceneManager.LoadScene(0);

    }
}
