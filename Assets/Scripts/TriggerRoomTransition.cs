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
    public float invokeTime = 3f;

    public GameObject signal;

    public GameObject wallCrack;

    public bool setTriggerActive = false;

    public bool setWallCrackActive = true;

    bool playSFX = false;

    bool trigger = false;



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
        print("Distance to player before if statement: " + distanceToPlayer);
        //print("Enemies in level: " + enemyCount);
        if (distanceToPlayer <= maxDistance && enemyCount <= 0)
        {
            Invoke("InitiateTrigger", invokeTime);
        }

    }

    private void OnCollisionEnter(Collision collider)
    {
        print("enemies in level: " + enemyCount);
        if (collider.gameObject.CompareTag("Player") && enemyCount <= 0 && trigger)
        {
            // level has been beat, load to next level
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();

            gameObject.SetActive(setTriggerActive);
            // play a sound effect and load to the next level
            AudioSource.PlayClipAtPoint(wallDestroyedSFX, Camera.main.transform.position);


        }
    }

    private void InitiateTrigger() {
            //print("Distance to player in if statement: " + distanceToPlayer);
            if(setWallCrackActive) {
                wallCrack.SetActive(true);
            }


            trigger = true;

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
