using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthPickup;
    public GameObject timePickup;
    public GameObject armorPickup;
    public GameObject attackPickup;

    GameObject[] spawnPoints;
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawn");

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        foreach(GameObject spawnPoint in spawnPoints)
        {
            int r = Random.Range(1, 100);
            if(r <= 40)
            {
                Instantiate(healthPickup, spawnPoint.transform.position, Quaternion.identity);
            }
            else if(r > 40 && r <= 60)
            {
                Instantiate(attackPickup, spawnPoint.transform.position, Quaternion.identity);
            }
            else if (r > 60 && r <= 80)
            {
                Instantiate(armorPickup, spawnPoint.transform.position, Quaternion.identity);
            }
            else if (r > 80)
            {
                Instantiate(timePickup, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }
}
