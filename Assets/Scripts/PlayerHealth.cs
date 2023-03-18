using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public AudioClip damageSFX;
    public AudioClip deadSFX;
    public int startHealth = 100;
    public Slider healthSlider;
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    public PlayerController pc;
    float durationTimer;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
        healthSlider.value = currentHealth;
        // start with alpha as 0
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        TestDamage();
        // fade out overlay
        if (overlay.color.a > 0)
        {
            // always stay if less than 25 hp
            if (currentHealth <= 25) return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                // fade out the overlay
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.7f);
        if (currentHealth > 0)
        {
            AudioSource.PlayClipAtPoint(damageSFX, transform.position);
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
            durationTimer = 0;
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        // kill player in player controller
        pc.KillPlayer();
        // call function on some levelmanager to lose

    }

    void TestDamage()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(10);
        }
    }
}
