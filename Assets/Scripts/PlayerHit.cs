using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public int skeletonDamage = 20;
    public float damageCoolDown = 1.0f;
    PlayerHealth ph;
    bool damageReady;

    // Start is called before the first frame update
    void Start()
    {
        ph = FindObjectOfType<PlayerHealth>();
        damageReady = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision obj)
    {
        if (damageReady)
        {
            if (obj.gameObject.CompareTag("EnemyWeaponSkeleton"))
            {
                ph.TakeDamage(skeletonDamage); //remove this call and put it in the attack state of the enemy
                damageReady = false;
                Invoke("ResetDamageCooldown", damageCoolDown);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (damageReady)
        {
            if (col.CompareTag("EnemyWeaponSkeleton"))
            {
                print("trigger damage");
                ph.TakeDamage(skeletonDamage); //remove this call and put it in the attack state of the enemy
                damageReady = false;
                Invoke("ResetDamageCooldown", damageCoolDown);
            }
        }
    }

    void ResetDamageCooldown()
    {
        damageReady = true;
    }
}
