using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    EntityMovement entityMovement;
    EntityGraphics entityGraphics;
    [SerializeField]
    LayerMask groundLayerMask;
    [SerializeField]
    bool raycastSuelo;

    void Start(){
        entityMovement = GetComponent<EntityMovement>();
        entityGraphics = GetComponent<EntityGraphics>();
    }

    private void OnEnable() {
        GetComponent<EntityMovement>().movementStateChangeEvent += UpdateGraphics;
    }

    private void OnDisable() {
        GetComponent<EntityMovement>().movementStateChangeEvent -= UpdateGraphics;
    }

    private void Update() {
        RaycastHit2D groundRaycast = Physics2D.Raycast(transform.position, Vector2.down, 1, groundLayerMask);
        if(groundRaycast) raycastSuelo = true;
        else raycastSuelo = false;
    }

    void FixedUpdate(){
        entityMovement.Move();
    }

    private void UpdateGraphics(){
        entityGraphics.UpdateEntityGraphics(entityMovement.entityMovementState, entityMovement.Velocity);
    }

    public EntityMovementState GetEntityMovementState(){
        return entityMovement.entityMovementState;
    }


}
