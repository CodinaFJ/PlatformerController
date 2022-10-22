using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [HideInInspector]
    public EntityMovementState entityMovementState;
    [SerializeField]
    protected Rigidbody2D myRigidbody2D;
    
    [SerializeField]
    protected LayerMask groundLayerMask;
    [SerializeField]
    protected bool onGround;

    public Action movementStateChangeEvent;

    protected float velocity = 0;
    public float Velocity{
        get => velocity;
    }

    protected virtual void Start(){
    }

    protected virtual void Update() {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, 1, groundLayerMask);
    }

    public virtual void Move(){

    }
    public virtual void Jump(){

    }
    
}

public enum EntityMovementState{
    Idle, Accelerate, Decelerate, Turn, MaxSpeed, Jump
}
