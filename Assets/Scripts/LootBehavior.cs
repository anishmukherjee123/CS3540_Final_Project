using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    public string identity;

    public int healthAdded = 10;
    public int shieldAdded = 20;
    public int attackAdded = 5;

    WeaponCollision weaponCollision;

    void Start()
    {
        weaponCollision = GameObject.FindObjectOfType<WeaponCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch(identity)
            {
                case "health":
                    HealthBehavior(other.gameObject);
                    break;
                case "attack":
                    AttackBehavior(other.gameObject);
                    break;
                case "time":
                    TimeBehavior(other.gameObject);
                    break;
                case "defense":
                    DefenseBehavior(other.gameObject);
                    break;
            }

            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);


            Destroy(gameObject, .5f);
        }
    }

    private void HealthBehavior(GameObject player)
    {
        player.GetComponent<PlayerHealth>().AddHealth(healthAdded);
    }
    private void AttackBehavior(GameObject player)
    {
        weaponCollision.damage += attackAdded;
    }
    private void DefenseBehavior(GameObject player)
    {
        player.GetComponent<PlayerHealth>().addShield(shieldAdded);
    }
    private void TimeBehavior(GameObject player)
    {

    }
}
