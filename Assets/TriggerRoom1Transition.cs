using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoom1Transition : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip wallDestroyedSFX;

    int enemyCount;
    void Start()
    {
        enemyCount = LevelManager.enemiesInLevel;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = LevelManager.enemiesInLevel;
    }

    private void OnCollisionEnter(Collision collider) {
        
        print("enemies in level: " + enemyCount);
        if(collider.gameObject.CompareTag("Player") && enemyCount <= 0) {
            Destroy(gameObject);
            // play a sound effect and load to the next level
            AudioSource.PlayClipAtPoint(wallDestroyedSFX, Camera.main.transform.position);
            // level has been beat, load to next level
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();
            
        }
    }
}
