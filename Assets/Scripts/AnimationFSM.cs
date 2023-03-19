using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFSM : MonoBehaviour
{
    public enum FSMStates {
        Idle,
        Attack,
        Climbing,
        Jump,
        Running
    }

    public FSMStates currentState;
    public KeyCode attackKey = KeyCode.R;
    Animator anim;
    bool playerState = true; // true for the player is alive and false for the player isn't alive
    float moveHorizInput;
    float moveVerticalInput;

    PlayerController pc;

    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null) {
            anim = GetComponent<Animator>();
        }

        if(pc == null) {
            pc = FindObjectOfType<PlayerController>();
        }
        currentState = FSMStates.Idle;

        anim.SetInteger("animState", 0);

        playerPos = transform.position;
  

    }

    // Update is called once per frame
    void Update()
    {
        // set the running animation maybe in
        // playercontroller when the
        // player is moving
        moveHorizInput = Input.GetAxis("Horizontal");
        moveVerticalInput = Input.GetAxis("Vertical");
        playerPos = transform.position;


        // instead of dead state
        // could also put everything
        // in the switch statement
        // in an if statement where it
        // checks the playerState to
        // see if the player is dead
        // and if so end the game and play
        // dead animation
        if(playerState) {
            switch(currentState) {
                case FSMStates.Attack:
                    UpdateAttackState();
                    break;
                case FSMStates.Climbing:
                    UpdateClimbingState();
                    break;
                case FSMStates.Jump:
                    UpdateJumpState();
                    break;
                case FSMStates.Running:
                    UpdateRunningState();
                    break;
                default:
                    UpdateIdleState();
                    break;
            }
        } else {
            // game over, player dies
            anim.SetInteger("animState", 6);
        }
    }

    void UpdateIdleState() {
        anim.SetInteger("animState", 0);
        if(checkAttackKey()) {
            currentState = FSMStates.Attack;
        } else if(pc.climbing) {
            currentState = FSMStates.Climbing;
        } else if(checkJumpKey()) {
            currentState = FSMStates.Jump;
        } else if(checkMovement()) {
            currentState = FSMStates.Running;
        }
    }
    void UpdateAttackState() {
        anim.SetInteger("animState", 1);
        if(!checkAttackKey()) {
            Invoke("changeToIdle", 2);
        }
        // need a script for the weapon
        // prefab where it checks
        // what it has collided with
        // and inflicts damage
    }

    void UpdateClimbingState() {
        if(FindObjectOfType<PlayerController>().grounded) {
            currentState = FSMStates.Idle;
        }
        
        float curr_y = transform.position.y;
        
        if(curr_y > playerPos.y) { // climbing in the upwards direction
            anim.SetInteger("animState", 3);
        } else if(curr_y < playerPos.y) { // climbing in the downwards direction
            anim.SetInteger("animState", 4);    
        }
    }

    void UpdateJumpState() {
        anim.SetInteger("animState", 7);
        if(!checkJumpKey()) {
            Invoke("changeToIdle", 2);
        }
    }
    // if can't fix the pause with switching to jump and attack animation
    // then just change the integer in the update methods
    // where attack or run might happen

    void UpdateRunningState() {
        anim.SetInteger("animState", 2);
        if(checkAttackKey()) {
            currentState = FSMStates.Attack;
        } else if(checkJumpKey()) {
            currentState = FSMStates.Jump;
        } else if(pc.climbing) {
            currentState = FSMStates.Climbing;
        }
    }

    // helpers

    private void changeToIdle() {
        currentState = FSMStates.Idle;
    }
    private bool checkMovement() {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
        Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D);
    }

    private bool checkAttackKey() {
        return Input.GetKeyDown(attackKey);
    }

    private bool checkJumpKey() {
        return Input.GetKeyDown(pc.jumpKey);
    }
}

