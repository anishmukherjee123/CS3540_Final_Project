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
            if(r <= 20)
            {

            }
        }
    }
}
