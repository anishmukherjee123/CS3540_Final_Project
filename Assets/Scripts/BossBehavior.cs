using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5;
    public float minDistance = 0;
    public int damageAmount = 20;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    public Animator anim;

    int currentDestinationIndex = 0;

    bool attackTurn;
    bool bossDead;

    void Start()
    {
        //set the wander points, get the animator and set it to walking
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        currentDestinationIndex = 0;
        FindNextPoint();

        anim.SetInteger("animState", 4);

        attackTurn = false;
        bossDead = false;

        //finds the player tag if not null
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //will switch the attack turn throughout the level
        InvokeRepeating("SwitchTurn", 5, 5);
    }


    void Update()
    {
        if (attackTurn)
        {
            //if the spider is attacking, will set the animation to attack
            //will also make the spider approach the player
            anim.SetInteger("animState", 1);

            float step = moveSpeed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > minDistance)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }
        }
        else if (!attackTurn && !bossDead)
        {
            //if spider is not attacking and it's not dead,
            //make it walk to the next wanderpoint
            anim.SetInteger("animState", 4);

            // walk to the next point if we have reached the current wanderpoint
            if (Vector3.Distance(transform.position, nextDestination) < 0.1f)
            {
                FindNextPoint();
            }
            else
            {
                transform.LookAt(nextDestination);
                transform.position = Vector3.MoveTowards(transform.position, nextDestination, moveSpeed * Time.deltaTime);
            }
        }
        else if (bossDead)
        {
            //if the spider is dead, do the dead animation and then destroy the object
            anim.SetInteger("animState", 2);
            Destroy(gameObject, 2);
            GameObject.FindObjectOfType<LevelManager>().LevelBeat();
        }

    }

    //switches the attack turn
    void SwitchTurn()
    {
        if (attackTurn)
        {
            attackTurn = false;
        }
        else
        {
            attackTurn = true;
        }

    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        print("Next destination: " + wanderPoints[currentDestinationIndex].gameObject.name);

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //damages the player
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
        else if (other.CompareTag("PlayerWeapon"))
        {
            //if hit by projectile, kills the boss
            //not sure what the tag for the attack weapon is, so that can be edited too
            bossDead = true;
        }
    }
}
