using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public AudioClip deathSFX;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        anim.Play("Hit", 0);
        anim.SetInteger("animState", 3);
        Destroy(gameObject, 2.25f);
    }
}
