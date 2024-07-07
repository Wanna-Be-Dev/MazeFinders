using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject dificulty;


    void Start()
    {
        PlayerPrefs.SetInt("playerDiff", 10);
        ActivateMainMenu(true);
        ActivateCursor();
       
    }
    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ActivateMainMenu(bool state)
    {
        mainMenu.SetActive(state);
        dificulty.SetActive(!state);
    }

    public void SetDif(int value)
    {
        switch (value)
        {
            case 0:
                PlayerPrefs.SetInt("playerDiff", 10);
                break;
            case 1:
                PlayerPrefs.SetInt("playerDiff", 30);
                break;
            case 2:
                PlayerPrefs.SetInt("playerDiff", 50);
                break;
        }
        
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
