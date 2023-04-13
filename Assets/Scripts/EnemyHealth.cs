using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deathSFX;
    public Slider healthBar;
    public float damageCooldown = 0.75f;
    bool damageReady = true;

    public int currentHealth;

    Animator anim;

    public bool dead = false;
    private void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthBar.value = currentHealth;
        }
        if (currentHealth <= 0 && !dead)
        {
            Death();
        }
    }

    void Death()
    {
        dead = true;
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        anim.SetInteger("animState", 4);
        LevelManager.enemiesInLevel--;
        Destroy(gameObject, 2.25f);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("PlayerWeapon") && damageReady)
        {
            damageReady = false;
            Invoke(nameof(ResetDamageCooldown), damageCooldown);
            TakeDamage(obj.gameObject.GetComponent<WeaponCollision>().damage);
        }
    }

    void ResetDamageCooldown()
    {
        damageReady = true;
    }
}
