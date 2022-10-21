using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    EntityMovement entityMovement;
    EntityGraphics entityGraphics;

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

    void Update(){
        entityMovement.Move();
    }

    private void UpdateGraphics(){
        entityGraphics.UpdateEntityGraphics(entityMovement.entityMovementState, entityMovement.Velocity);
    }

    public EntityMovementState GetEntityMovementState(){
        return entityMovement.entityMovementState;
    }


}
