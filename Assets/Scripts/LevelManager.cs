using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public string nextLevel;
    public static int enemiesInLevel = 0;
    public float invokeTime = 1f;
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
        Invoke("LoadNextLevel", invokeTime);
        //LoadNextLevel();
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