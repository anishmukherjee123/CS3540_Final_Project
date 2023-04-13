using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    public Transform player;
    public float moveSpeed = 5;
    public float minDistance = 0;
    public int damageAmount = 20;
    public Animator anim;
    public AudioClip deadSFX;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    BossHealth BossHealth;
    int health;

    int currentDestinationIndex = 0;

    bool attackTurn;

    bool playSFX = false;

    void Start()
    {
        //set the wander points, get the animator and set it to walking
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentDestinationIndex = 0;

        BossHealth = GetComponent<BossHealth>();
        health = BossHealth.currentHealth;

        anim.SetInteger("animState", 4);
        FindNextPoint();

        attackTurn = false;

        //will switch the attack turn throughout the level
        InvokeRepeating("SwitchTurn", 8, 5);
    }


    void Update()
    {

        if (attackTurn)
        {

            anim.SetInteger("animState", 1);

            float step = moveSpeed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > minDistance)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }
        }
        else if (!attackTurn)
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

        health = BossHealth.currentHealth;

        if (health <= 0)
        {
            SpiderDeath();
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

    void SpiderDeath()
    {
        if(!playSFX) {
            AudioSource.PlayClipAtPoint(deadSFX, Camera.main.transform.position);
            playSFX = true;
        }
        anim.SetInteger("animState", 2);
        Destroy(gameObject, 2);
    }
}
