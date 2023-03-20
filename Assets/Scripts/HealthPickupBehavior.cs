using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupBehavior : MonoBehaviour
{
    public int healthAdded = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().AddHealth(healthAdded);
        }
    }
}
