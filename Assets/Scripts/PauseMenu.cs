using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;

    CinemachineFreeLook cineCam;

    void Start()
    {
        cineCam = GameObject.FindGameObjectWithTag("ThirdPersonCamera").GetComponent<CinemachineFreeLook>();

        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        cineCam.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        cineCam.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        ResumeGame();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenControlsMenu()
    {
        controlsMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void CloseControlsMenu()
    {
        controlsMenu.SetActive(false);
    }

    public void SetMouseSensitivity(float sens)
    {
        cineCam.m_XAxis.m_MaxSpeed = 3 * sens;
        cineCam.m_YAxis.m_MaxSpeed = sens / 3;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }
}
