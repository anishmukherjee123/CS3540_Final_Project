using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateEnemies : MonoBehaviour
{
    [SerializeField] public UnityEvent _update;

    // Update is called once per frame
    void Update()
    {
        _update.Invoke();
    }
}
