using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code adapted from: https://www.youtube.com/watch?v=tAJLiOEfbQg
public class Climbing : MonoBehaviour
{
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;

    [Header("ClimbJump")]
    public KeyCode climbJumpKey = KeyCode.Space;
    public float climbJumpSpeed = 5.0f;
    public float climbJumpDistance = 5.0f;

    public float climbSpeed;
    private bool climbing;

    public float detectionLength;
    public float sphereCastRadius;

    private RaycastHit frontWallHit;
    private bool wallFront;

    PlayerController pc;

    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        StateMachine();
        if (climbing)
        {
            ClimbingMovement();
            HandleClimbJump();
        }
    }

    private void StateMachine()
    {
        if (wallFront && Input.GetMouseButtonDown(1) && !climbing)
        {
            if (!climbing)
            {
                StartClimbing();
            }
        }
        else
        {
            if ((climbing && Input.GetMouseButtonDown(1)))
            {
                StopClimbing();
            }
        }
    }

    void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
    }

    private void StartClimbing()
    {
        climbing = true;
        pc.climbing = true;
        rb.useGravity = false;
        // no longer going to slide down the wall

        // camera fov changing

        // set up a climbing timer with a graphic like BOtW
    }

    private void ClimbingMovement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // align our movement axes with the wall we are climbing
        Vector3 horizontalAxis = Vector3.Cross(frontWallHit.normal, orientation.up);
        Vector3 verticalAxis = Vector3.Cross(frontWallHit.normal, orientation.right);

        Vector3 moveDirection = verticalAxis * verticalInput * -1 + horizontalAxis * horizontalInput;
        rb.velocity = moveDirection * climbSpeed;

        rb.velocity -= frontWallHit.normal * 0.9f;
    }

    private void HandleClimbJump()
    {
        if (Input.GetKeyDown(climbJumpKey))
        {
            StopClimbing();
            Vector3 verticalAxis = Vector3.Cross(frontWallHit.normal, orientation.right);
            rb.AddForce((-1 * verticalAxis * climbJumpSpeed) - (frontWallHit.normal * 0.2f), ForceMode.Impulse);
        }
    }

    private void StopClimbing()
    {
        climbing = false;
        pc.climbing = false;
        rb.useGravity = true;

        // some effect when we are done climbing
    }
}

