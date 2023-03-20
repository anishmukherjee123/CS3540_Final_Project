using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3;
    public float chaseRadius = 40.0f;
    public float attackRadius = 20.0f;
    public float attackCooldown = 1.0f;
    Animator anim;
    bool readyToAttackPlayer;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        readyToAttackPlayer = true;
        anim = GetComponent<Animator>();
        // base state is idling
        anim.SetInteger("animState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= chaseRadius && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            // chase the player if in radius
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRadius && readyToAttackPlayer)
        {
            // attack the player
            AttackPlayer();
            readyToAttackPlayer = false;
            Invoke(nameof(ResetAttackCooldown), attackCooldown);
        }
        else if (Vector3.Distance(transform.position, player.position) > attackRadius)
        {
            // set running animation
            anim.SetInteger("animState", 1);
            Vector3 playerNoY = new Vector3(player.position.x, transform.position.y, player.position.z);
            // look at x/z of the player
            transform.LookAt(playerNoY);
            // chase player
            transform.position = Vector3.MoveTowards(transform.position, playerNoY, moveSpeed * Time.deltaTime);
        }
    }

    void AttackPlayer()
    {
        // play attack animation
        anim.SetInteger("animState", 2);
    }

    void ResetAttackCooldown()
    {
        readyToAttackPlayer = true;
    }
}
