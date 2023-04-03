using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoom1Transition : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip wallDestroyedSFX;
    public GameObject player;

    public float maxDistance = 50f;

    int enemyCount;
    void Start()
    {
        enemyCount = LevelManager.enemiesInLevel;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = LevelManager.enemiesInLevel;

        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        print("Distance to player before if statement: " + distanceToPlayer);
        if(distanceToPlayer <= maxDistance && enemyCount <= 0) {
            print("Distance to player in if statement: " + distanceToPlayer);
            Renderer renderer = gameObject.GetComponent<Renderer>();
            Color emissiveColor = Color.white;
            float emissiveIntesnity = 10f;
            //m_EmissiveObject.GetComponent<Renderer>().material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);
            renderer.material.SetColor("_EmissiveColor", emissiveColor * emissiveIntesnity);
        }
        // could calcuate the distance between the player and the endpt
        // when the player is within a certain distance
        // give them some kind of signal (make the wall glow or an arrow)
        
    }

    private void OnCollisionEnter(Collision collider) {
        
        print("enemies in level: " + enemyCount);
        if(collider.gameObject.CompareTag("Player") && enemyCount <= 0) {
            gameObject.SetActive(false);
            // play a sound effect and load to the next level
            AudioSource.PlayClipAtPoint(wallDestroyedSFX, Camera.main.transform.position);
            // level has been beat, load to next level
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();
            
        }
    }
}
