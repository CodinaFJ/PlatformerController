using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityMovement, IObserver
{
    [SerializeField][Range(1,100)]
    private float maxSpeed = 10;
    [SerializeField][Range(1,100)]
    private float acceleration = 10;
    [SerializeField][Range(1,100)]
    private float deceleration = 10;
    [SerializeField][Range(1,100)]
    private float turnAcceleration = 10;
    [SerializeField][Range(0.01f, 1)]
    private float stopBuffer = 0.1f;
    [SerializeField]
    private bool instantMovement = false;

    float acc = 0;
 
    //MoveInput: Vector2 that maps LEFT key in X coordinate and RIGHT key in Y coordinate.
    //Value goes to 1 on press and to 0 on release. Posible values are:
    //  (0,0) Idle
    //  (1,0) Left pressed
    //  (0,1) Right pressed
    //  (1,1) Both pressed
    private Vector2 moveInput;

    protected override void Start() {
        base.Start();
        InputManager.Instance.RegisterObserver(this);
    }

    public void UpdateObserver(){
        moveInput = InputManager.Instance.MoveInput;
        SetAcceleration();
    }

    private void SetAcceleration(){
        if(moveInput.magnitude == 1){
            if(instantMovement) velocity = -moveInput.x * maxSpeed + moveInput.y * maxSpeed;
            else if(Mathf.Sign(velocity) == Mathf.Sign(-moveInput.x + moveInput.y) || Mathf.Abs(velocity) == 0){
                //ACCELERATE
                entityMovementState = EntityMovementState.Accelerate;
                acc = -moveInput.x * acceleration + moveInput.y * acceleration;
            }
            else{
                //TURN
                entityMovementState = EntityMovementState.Turn;
                acc = - moveInput.x * turnAcceleration + moveInput.y * turnAcceleration;
            }

            if(velocity == 0) velocity = Mathf.Epsilon * (-moveInput.x + moveInput.y);
        } 
        else{
            if(Mathf.Abs(velocity) < stopBuffer || instantMovement){
                //STOP
                entityMovementState = EntityMovementState.Idle;
                acc = 0;
                velocity = 0;
            }
            else{
                //DECELERATE
                entityMovementState = EntityMovementState.Decelerate;
                acc = -Mathf.Sign(velocity) * deceleration;
            }
        }
        movementStateChangeEvent?.Invoke(); 
    }

    public override void Move(){
        float pos_0 = transform.position.x;
        float pos_1 = 0;
        float vel_0 = velocity;
        float vel_1 = 0;

        vel_1 = Mathf.Clamp(vel_0 + Time.deltaTime * acc, -maxSpeed, maxSpeed);
        if(Mathf.Abs(vel_1) == maxSpeed) entityMovementState = EntityMovementState.MaxSpeed;

        pos_1 = pos_0 + Time.deltaTime * Mathf.Clamp(vel_1 + 0.5f * acc * Time.deltaTime, -maxSpeed ,maxSpeed);
        
        transform.position = new Vector2(pos_1, transform.position.y);
        
        velocity = (pos_1 - pos_0)/Time.deltaTime;

        if(Mathf.Abs(velocity) < stopBuffer) SetAcceleration();
    }

    ~PlayerMovement(){
        InputManager.Instance.RemoveObserver(this);
    }

}
