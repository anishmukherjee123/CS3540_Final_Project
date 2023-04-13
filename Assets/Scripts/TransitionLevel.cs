using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLevel : MonoBehaviour
{

    bool dead = false;
    // Update is called once per frame
    void Update()
    {
        if(BossHealth.currentHealth <= 0 && !dead) {
            LevelManager.enemiesInLevel--;
            dead = true;
        }
        
        if(LevelManager.enemiesInLevel <= 0) {
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
