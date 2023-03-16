using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PLayer"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);


            Destroy(gameObject, .5f);
        }
    }
}
