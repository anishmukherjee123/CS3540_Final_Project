using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")) {
            // enemy takes damage
        }
    }
}
