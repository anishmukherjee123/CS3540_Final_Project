using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRise : MonoBehaviour
{
    public float moveSpeed = 5;
    public Transform endPos;

    bool active = false;
    bool hasPlayer = false;

    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        if (hasPlayer)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, step);
        }
        else if(!hasPlayer && transform.position.y > startPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
        }
    }

    public void Activate()
    {
        Debug.Log("activated");
        active = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && active)
        {
            Debug.Log("Has player");
            collision.gameObject.transform.SetParent(transform, true);
            hasPlayer= true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (active)
        {
            transform.DetachChildren();
            hasPlayer = false;
        }
    }
}
