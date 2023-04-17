using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Text gameText;

    public TextMeshProUGUI enemiesLeftTxt;
    public string levelBeatString;
    public string levelLostString;
    public string nextLevel;
    public static int enemiesInLevel = 0;
    public float levelBeatInvokeTime = 1f;
    public float levelLostInvokeTime = 1f;

    public bool lastLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //print("The enemies in the level: " + enemiesInLevel);
        setEnemiesLeftText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }

        // keeping this incase the player wants to restart
        // the level
        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadCurrentLevel();
        }

        setEnemiesLeftText();
    }

    public void setEnemiesLeftText()
    {
        //print("Enemies in level: " + enemiesInLevel);
        //if (enemiesInLevel < 0)
        //{
           // enemiesLeftTxt.text = "Enemies Left:  " + 0;
        //}
        //else
        //{
            enemiesLeftTxt.text = "Enemies Left:  " + enemiesInLevel.ToString();
        //}
    }

    public void LevelBeat()
    {
        print("LevelBeat has been called");
        gameText.text = levelBeatString;

        gameText.gameObject.SetActive(true);
        if (!lastLevel)
        {
            Invoke("LoadNextLevel", levelBeatInvokeTime);
        }
    }

    public void LevelLost()
    {
        gameText.text = levelLostString;

        gameText.gameObject.SetActive(true);

        Invoke(nameof(LoadCurrentLevel), levelLostInvokeTime);
    }




    void LoadNextLevel()
    {
        print("LoadNextLevel called with: " + nextLevel);
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}