using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2BossHealth : MonoBehaviour
{
    public int startingHealth = 100;

    public static int currentHealth;

    public Slider healthSlider;

    public AudioClip hitSFX;

    private FinalBossBehavior behavior;

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        behavior = FindObjectOfType<FinalBossBehavior>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth-damageAmount > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        else
        {
            currentHealth = 0;
            healthSlider.value = 0;
            Debug.Log("Boss dead");
            behavior.Die();
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            Debug.Log("boss hit by weapon");
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position);
            TakeDamage(10);
        }
    }
}
