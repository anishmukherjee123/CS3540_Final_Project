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
            if(r <= 10)
            {
                GameObject health = Instantiate(healthPickup, spawnPoint.transform.position, Quaternion.identity);
                health.transform.parent = spawnPoint.transform;
            }
            else if(r > 10 && r <= 15)
            {
                GameObject attack = Instantiate(attackPickup, spawnPoint.transform.position, Quaternion.identity);
                attack.transform.parent = spawnPoint.transform;
            }
            else if (r > 15 && r <= 20)
            {
                GameObject defense = Instantiate(armorPickup, spawnPoint.transform.position, Quaternion.Euler(270, 0, 0));
                defense.transform.parent = spawnPoint.transform;
            }
            else if (r > 20 && r <= 25)
            {
                GameObject time = Instantiate(timePickup, spawnPoint.transform.position, Quaternion.Euler(0, 0, 90));
                time.transform.parent = spawnPoint.transform;
            }
        }
    }
}
