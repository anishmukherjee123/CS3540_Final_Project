using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    GameObject settingsMenu;
    GameObject controlsMenu;
    GameObject aboutMenu;

    void Start()
    {
        settingsMenu = GameObject.FindGameObjectWithTag("SettingsMenu");
        settingsMenu.SetActive(false);

        controlsMenu = GameObject.FindGameObjectWithTag("ControlsMenu");
        controlsMenu.SetActive(false);

        aboutMenu = GameObject.FindGameObjectWithTag("AboutMenu");
        aboutMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        CloseControlsMenu();
        CloseAboutMenu();
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenControlsMenu()
    {
        controlsMenu.SetActive(true);
        CloseSettingsMenu();
        CloseAboutMenu();
    }

    public void CloseControlsMenu()
    {
        controlsMenu.SetActive(false);
    }

    public void OpenAboutMenu()
    {
        aboutMenu.SetActive(true);
        CloseSettingsMenu();
        CloseControlsMenu();
    }

    public void CloseAboutMenu()
    {
        aboutMenu.SetActive(false);
    }
}
