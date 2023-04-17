using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Level2BossBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float minDistance = 0;
    public int damageAmount = 20;
    public Animator anim;
    public AudioClip deadSFX;
    public AudioClip attackSFX;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Level2BossHealth Level2BossHealth;
    int health;

    int currentDestinationIndex = 0;

    bool attackTurn;

    void Start()
    {
        //set the wander points, get the animator and set it to walking
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDestinationIndex = 0;

        Level2BossHealth = GetComponent<Level2BossHealth>();
        health = Level2BossHealth.currentHealth;

        FindNextPoint();

        attackTurn = false;

        //will switch the attack turn throughout the level
        InvokeRepeating("SwitchTurn", 10, 8);

        InvokeRepeating("PlayAudio", 0, 3);
    }


    void Update()
    {

        if (attackTurn)
        {
            print(player);

            float step = moveSpeed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > minDistance)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }

            if (distance < 2)
            {
                anim.SetInteger("animState", 1);
            }
        }
        else if (!attackTurn)
        {

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

        health = Level2BossHealth.currentHealth;

        if (health <= 0)
        {
            BossDeath();
            FindObjectOfType<LevelManager>().LevelBeat();
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
    }

    void BossDeath()
    {
        anim.SetInteger("animState", 2);
        Destroy(gameObject, 2);
    }

    void PlayAudio()
    {
        if (attackTurn)
        {
            AudioSource.PlayClipAtPoint(attackSFX, Camera.main.transform.position);
        }
    }
}
