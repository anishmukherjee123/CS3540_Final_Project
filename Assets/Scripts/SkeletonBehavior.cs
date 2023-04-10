using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static AnimationFSM;

public class SkeletonBehavior : MonoBehaviour
{

    public enum FSM_states
    {
        Patrol,
        Chase, 
        Attack
    }

    public Transform player;

    public float moveSpeed = 3;

    public float chaseRadius = 40.0f;
    public float attackRadius = 20.0f;
    public float attackCooldown = 1.0f;

    public GameObject wanderPointParent;

    public Transform enemyEyes;
    public float fieldOfView = 45;

    Animator anim;

    bool readyToAttackPlayer;

    FSM_states state;

    List<GameObject> wanderPoints;
    Vector3 nextDest;

    int curDestIndex = 0;

    float distanceToPlayer;
    float elapsedTime = 0;

    EnemyHealth enemyHealth;
    int health;

    Transform deadTransform;
    bool isDead;

    NavMeshAgent agent;

    //attack range on enemy is too far

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //add this enemies wanderpoints 
        wanderPoints = new List<GameObject>();
        foreach (Transform transform in wanderPointParent.transform)
        {
            if (transform.CompareTag("WanderPoint"))
            {
                wanderPoints.Add(transform.gameObject);
            }
        }

        readyToAttackPlayer = true;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // base state is patrolling
        state = FSM_states.Patrol;
        FindNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch(state)
        {
            case FSM_states.Patrol:
                UpdatePatrolState();
                break;
            case FSM_states.Chase:
                UpdateChaseState();
                break;
            case FSM_states.Attack:
                UpdateAttackState();
                break;
        }
        elapsedTime += Time.deltaTime;

    }

    void UpdatePatrolState()
    {
        anim.SetInteger("animState", 1);

        agent.stoppingDistance = 2;

        if(Vector3.Distance(transform.position, nextDest) < 2)
        {
            //Debug.Log("Looks for next point");
            FindNextPoint();
        }
        else if(IsPlayerInClearFov() && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            //Debug.Log("Sees player");
            state = FSM_states.Chase;
        }

        //Debug.Log(gameObject.name + ": " + nextDest.ToString());
        FaceTarget(nextDest);
        agent.SetDestination(nextDest);
    }

    void UpdateChaseState()
    {
        anim.SetInteger("animState", 1);

        agent.stoppingDistance = attackRadius;
        nextDest = player.position;
        //Debug.Log("in chase state. NextDest: " + nextDest);

        if(distanceToPlayer <= attackRadius)
        {
            state = FSM_states.Attack;
        }
        else if(distanceToPlayer > chaseRadius)
        {
            FindNextPoint();
            state = FSM_states.Patrol;
        }

        FaceTarget(nextDest);
        agent.SetDestination(nextDest);
    }

    void UpdateAttackState()
    {
        nextDest = player.position;
        agent.stoppingDistance = 0;

        if (distanceToPlayer > attackRadius && distanceToPlayer <= chaseRadius)
        {
            state = FSM_states.Chase;
        }
        else if(distanceToPlayer > chaseRadius)
        {
            FindNextPoint();
            state = FSM_states.Patrol;
        }

        if(readyToAttackPlayer)
        {
            // attack the player
            AttackPlayer(); //change this so it directly damages the player when it attacks, rather than relying on colliders
            readyToAttackPlayer = false;
            Invoke(nameof(ResetAttackCooldown), attackCooldown);
        }
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void FindNextPoint()
    {
        nextDest = wanderPoints[curDestIndex].transform.position;
        curDestIndex = (curDestIndex + 1) % wanderPoints.Count;
       
        agent.SetDestination(nextDest);
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

    bool IsPlayerInClearFov()
    {
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        RaycastHit hit;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseRadius))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player in sight");
                    return true;
                }
                return false;
            }
            return false;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseRadius);
        //create a ray that is the front ray, but rotated around half of the field of view width 
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * .5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * .5f, 0) * frontRayPoint;

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);

    }
}
