using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    bool started = false;
    FinalBossBehavior behavior;
    void Start()
    {
        behavior = FindObjectOfType<FinalBossBehavior>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !started)
        {
            behavior.startShooting();
            started = true;
        }
    }
}
