using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text gameText;

    public string levelBeatString;
    public string levelLostString;
    public string nextLevel;
    public static int enemiesInLevel = 0;
    public float levelBeatInvokeTime = 1f;
    public float levelLostInvokeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy").Length;
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
    }

    public void LevelBeat() {
        gameText.text = levelBeatString;

        gameText.gameObject.SetActive(true);

        Invoke("LoadNextLevel", levelBeatInvokeTime);
    }

    public void LevelLost() {
        gameText.text = levelLostString;

        gameText.gameObject.SetActive(true);

        Invoke(nameof(LoadCurrentLevel), levelLostInvokeTime);
    }


    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("Player") && enemiesInLevel == 0)
    //     {
    //         LoadNextLevel();
    //     }
    // }
}