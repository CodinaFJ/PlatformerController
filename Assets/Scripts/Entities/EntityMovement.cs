using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [HideInInspector]
    public EntityMovementState entityMovementState;
    Rigidbody2D myRigidbody2D;

    public Action movementStateChangeEvent;

    protected float velocity = 0;
    public float Velocity{
        get => velocity;
    }

    protected virtual void Start(){
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Move(){

    }
    
}

public enum EntityMovementState{
    Idle, Accelerate, Decelerate, Turn, MaxSpeed
}
