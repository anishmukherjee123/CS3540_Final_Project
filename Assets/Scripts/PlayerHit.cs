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

    void OnTriggerEnter(Collider col)
    {
        if (damageReady)
        {
            if (col.CompareTag("EnemyWeaponSkeleton"))
            {
                ph.TakeDamage(skeletonDamage);
                damageReady = false;
                Invoke("ResetDamageCooldown", damageCoolDown);
            }
            if(col.CompareTag("Projectile"))
            {
                ph.TakeDamage(10);
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
