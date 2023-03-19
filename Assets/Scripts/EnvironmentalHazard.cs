using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalHazard : MonoBehaviour
{
    public PlayerHealth ph;
    // Start is called before the first frame update
    void Start()
    {
        ph = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // kill player when it collides
    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            // kill the player if they are alive
            if (ph.currentHealth > 0)
            {
                ph.TakeDamage(ph.startHealth);
            }
        }
    }
}
