using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code adapted from: https://www.youtube.com/watch?v=tAJLiOEfbQg
public class Climbing : MonoBehaviour
{
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;

    public float climbSpeed;
    private bool climbing;

    // jumping
    public float climbJumpUpForce;
    public KeyCode jumpKey = KeyCode.Space;

    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing) ClimbingMovement();
    }

    private void StateMachine()
    {
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {
            if (!climbing) StartClimbing();
        }
        else
        {
            if (climbing) StopClimbing();
        }

        if (wallFront && Input.GetKeyDown(jumpKey)) ClimbJump();
    }

    void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }

    private void StartClimbing()
    {
        climbing = true;
    }

    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }

    private void StopClimbing()
    {
        climbing = false;
    }

    private void ClimbJump()
    {
        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
