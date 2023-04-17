using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{

    bool dead = false;
    int bossHealth = 100;

    void Start() {
        if(SceneManager.GetActiveScene().name.Contains("Level2")) {
            bossHealth = Level2BossHealth.currentHealth;
            Debug.Log("this is level2 boss");
        } else {
            bossHealth = BossHealth.currentHealth;
            Debug.Log("this is level1 boss");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("The boss's current health: " + bossHealth);
        if(bossHealth <= 0 && !dead) {
            LevelManager.enemiesInLevel--;
            dead = true;
            Debug.Log("Boss is dead");
        }
        
        if(LevelManager.enemiesInLevel <= 0 || dead) {
            Debug.Log("Next level called");
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
