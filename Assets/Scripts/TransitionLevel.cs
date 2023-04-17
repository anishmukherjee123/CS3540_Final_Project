using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{

    bool dead = false;
    int bossHealth = 100;

    void Start()
    {
        UpdateHealth();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        print("The boss's current health: " + bossHealth);
        if (bossHealth <= 0 && !dead)
        {
            LevelManager.enemiesInLevel--;
            dead = true;
        }

        if (LevelManager.enemiesInLevel <= 0)
        {
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();
        }
    }

    void UpdateHealth()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level2"))
        {
            bossHealth = Level2BossHealth.currentHealth;
        }
        else
        {
            bossHealth = BossHealth.currentHealth;
        }
    }
}
