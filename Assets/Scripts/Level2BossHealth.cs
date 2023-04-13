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

    private void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position);
            TakeDamage(10);
        }
    }
}
