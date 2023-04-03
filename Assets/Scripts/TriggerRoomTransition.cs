using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerRoomTransition : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip wallDestroyedSFX;
    public AudioClip wallCrackSFX;
    public GameObject player;

    public float maxDistance = 50f;

    public float lightIntensity = 10f;

    public GameObject signal;

    public GameObject wallCrack;

    bool playSFX = false;


    int enemyCount;
    void Start()
    {
        enemyCount = LevelManager.enemiesInLevel;
        player = GameObject.FindGameObjectWithTag("Player");
        if(signal == null) {
            signal = GameObject.FindGameObjectWithTag("Signal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = LevelManager.enemiesInLevel;

        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        //print("Distance to player before if statement: " + distanceToPlayer);
        if (distanceToPlayer <= maxDistance && enemyCount <= 0)
        {
            Invoke("InitiateTrigger", 3);
        }

    }

    private void OnCollisionEnter(Collision collider)
    {
        print("enemies in level: " + enemyCount);
        if (collider.gameObject.CompareTag("Player") && enemyCount <= 0)
        {
            gameObject.SetActive(false);
            // play a sound effect and load to the next level
            AudioSource.PlayClipAtPoint(wallDestroyedSFX, Camera.main.transform.position);
            // level has been beat, load to next level
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();

        }
    }

    private void InitiateTrigger() {
            //print("Distance to player in if statement: " + distanceToPlayer);
            wallCrack.SetActive(true);
            // add wall crack sound effect
            if(!playSFX) {
                AudioSource.PlayClipAtPoint(wallCrackSFX, Camera.main.transform.position);
                playSFX = true;
            }

            Light[] lights = signal.GetComponentsInChildren<Light>();

            foreach(Light eachLight in lights) {
                eachLight.intensity = lightIntensity;
            }
    }
}
