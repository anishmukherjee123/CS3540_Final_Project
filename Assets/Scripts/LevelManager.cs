using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)) {
            LoadNextLevel();
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            LoadCurrentLevel();
        }
    }

    
    void LoadNextLevel() {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
