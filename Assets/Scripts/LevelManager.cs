using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public string nextLevel;
    public static int enemiesInLevel = 0;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadCurrentLevel();
        }
    }

    public void LevelBeat() {
        //Invoke("LoadNextLevel", 2);
        LoadNextLevel();
    }


    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && enemiesInLevel == 0)
        {
            LoadNextLevel();
        }
    }
}