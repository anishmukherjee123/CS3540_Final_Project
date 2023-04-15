using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float climbSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float sprintingAirMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float attackCooldown = 1.0f;

    public float playerHeight;
    public LayerMask whatIsGround;
    public Transform orientation;
    public bool grounded;
    public bool climbing;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    bool readyToAttack;
    public bool playerAlive;

    [Header("Animations")]
    public Animator animator;

    GameObject hook;

    public enum MovementState
    {
        walking,
        climbing,
        sprinting,
        air
    }
    public enum AnimState
    {
        Idle,
        Attack,
        ClimbingUp,
        ClimbingDown,
        Jump,
        Running,
        Dead
    }

    public MovementState state;
    AnimState animState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animState = AnimState.Idle;

        hook = GameObject.Find("Hook");

        readyToJump = true;
        readyToAttack = true;
        playerAlive = true;
    }

    private void Update()
    {
        // grounded on either Ground layer or ClimbingWall layer
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround | (1 << LayerMask.NameToLayer("ClimbingWall")));

        if (playerAlive)
        {
            MyInput();
            SpeedControl();
            StateHandler();
            AnimHandler();
        }

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        if (!climbing && playerAlive)
        {
            MovePlayer();
        }
    }

    private void AnimHandler()
    {
        switch (animState)
        {
            case AnimState.Attack:
                animator.SetInteger("animState", 1);
                break;
            case AnimState.ClimbingUp:
                animator.SetInteger("animState", 3);
                break;
            case AnimState.ClimbingDown:
                animator.SetInteger("animState", 4);
                break;
            case AnimState.Jump:
                animator.SetInteger("animState", 7);
                break;
            case AnimState.Running:
                animator.SetInteger("animState", 2);
                break;
            case AnimState.Dead:
                animator.SetInteger("animState", 6);
                break;
            case AnimState.Idle:
                animator.SetInteger("animState", 0);
                break;
            default:
                break;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0) && readyToAttack && grounded && !climbing)
        {
            Attack();
            readyToAttack = false;
            Invoke(nameof(ResetAttack), attackCooldown);
        }

        if (horizontalInput == 0 && verticalInput == 0 && grounded && readyToJump && readyToAttack && !climbing)
        {
            animState = AnimState.Idle;
        }

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Jump();
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void StateHandler()
    {
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded && !climbing)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else if (climbing)
        {
            state = MovementState.climbing;
            moveSpeed = climbSpeed;
            animState = AnimState.ClimbingUp;
        }
        else if (!readyToJump) // currently jumping
        {
            state = MovementState.air;
        }
        else
        {
            state = MovementState.air;
            animState = AnimState.Jump;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded && readyToAttack && readyToJump)
        {
            animState = AnimState.Running;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void KillPlayer()
    {
        // play death animation
        animState = AnimState.Dead;
        animator.SetInteger("animState", 6);
        playerAlive = false;
    }

    private void Jump()
    {
        // reset y velocity before applying force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (!climbing)
        {
            animState = AnimState.Jump;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Attack()
    {
        hook.GetComponent<MeshCollider>().enabled = true;
        animState = AnimState.Attack;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetAttack()
    {
        hook.GetComponent<MeshCollider>().enabled = false;
        readyToAttack = true;
    }
}
