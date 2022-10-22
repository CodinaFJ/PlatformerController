using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityGraphics : MonoBehaviour
{
    Animator myAnimator;
    private string currentState;

    private const string IDLE = "idle";
    private const string RUN = "run";
    private const string CLIMB = "climb";
    private const string JUMP = "jump";
    private const string CROUCH = "crouch";

    void Start(){
        myAnimator = GetComponentInChildren<Animator>();
    }

    public void UpdateEntityGraphics(EntityMovementState entityMovementState, float velocity){
        float orientation = Mathf.Sign(velocity);
        if(velocity == 0) orientation = transform.localScale.x;

        switch(entityMovementState){
            case EntityMovementState.Idle:
                ChangeAnimationState(IDLE);
                transform.localScale = Vector3.one;
                break;

            case EntityMovementState.Accelerate:
                ChangeAnimationState(RUN);
                transform.localScale = new Vector3(orientation, 1, 1);
                break;

            case EntityMovementState.Decelerate:
                ChangeAnimationState(RUN);
                transform.localScale = new Vector3(transform.localScale.x, 1, 1);
                break;

            case EntityMovementState.MaxSpeed:
                ChangeAnimationState(RUN);
                transform.localScale = new Vector3(orientation, 1, 1);
                break;

            case EntityMovementState.Turn:
                ChangeAnimationState(RUN);
                transform.localScale = new Vector3(-orientation, 1, 1);
                break;

            default:
            break;
        }
    }

    public void ChangeAnimationState(string newState){
        //stop the same animation from interrupting itself
        if(currentState == newState) return;

        //play the animation
        myAnimator.Play(newState);
        //myAnimator.GetNextAnimatorStateInfo(0).

        //reassign the current state
        currentState = newState;
    }
}

public enum EntityOrientation{
    Left, Right
}
