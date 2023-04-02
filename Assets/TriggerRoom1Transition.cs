using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoom1Transition : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip wallDestroyedSFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collider) {
        if(collider.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            // play a sound effect and load to the next level
            AudioSource.PlayClipAtPoint(wallDestroyedSFX, Camera.main.transform.position);
            // level has been beat, load to next level
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();
            
        }
    }
}
