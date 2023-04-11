using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthPickup;
    public GameObject armorPickup;
    public GameObject attackPickup;

    public bool spawnAttack = false;
    public bool spawnDefense = false;
    public bool spawnHealth = true;

    GameObject[] spawnPoints;
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawn");

        Spawn();
    }

    void Spawn()
    {
        foreach(GameObject spawnPoint in spawnPoints)
        {
            int r = Random.Range(1, 100);
            if(r <= 10 && spawnHealth)
            {
                GameObject health = Instantiate(healthPickup, spawnPoint.transform.position, Quaternion.identity);
                health.transform.parent = spawnPoint.transform;
                health.GetComponent<LootBehavior>().identity = "health";
            }
            else if(r > 10 && r <= 15 && spawnAttack)
            {
                GameObject attack = Instantiate(attackPickup, spawnPoint.transform.position, Quaternion.identity);
                attack.transform.parent = spawnPoint.transform;
                attack.GetComponent<LootBehavior>().identity = "attack";
            }
            else if (r > 15 && r <= 20 && spawnDefense)
            {
                GameObject defense = Instantiate(armorPickup, spawnPoint.transform.position, Quaternion.Euler(270, 0, 0));
                defense.transform.parent = spawnPoint.transform;
                defense.GetComponent<LootBehavior>().identity = "defense";
            }
        }
    }
}
