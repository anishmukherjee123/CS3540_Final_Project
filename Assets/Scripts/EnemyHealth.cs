using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deathSFX;
    public Slider healthBar;

    public int currentHealth;

    Animator anim;

    bool dead = false;
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
        anim.Play("Hit", 0);
        anim.SetInteger("animState", 3);
        LevelManager.enemiesInLevel--;
        Destroy(gameObject, 2.25f);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage(obj.gameObject.GetComponent<WeaponCollision>().damage); 
        }
    }
}
