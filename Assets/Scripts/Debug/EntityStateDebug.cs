using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityStateDebug : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro movementStateText;
    private EntityBehavior entityBehavior;

    void Start()
    {
        try {
            entityBehavior = GetComponentInParent<EntityBehavior>();
            Debug.Log("EntityStateDebug Started Correctly");
        }
        catch {Debug.Log("Error starting components");}
    }

    void Update()
    {
        UpdateText();
        if(Mathf.Sign(entityBehavior.transform.localScale.x) < 1) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
    }

    private void UpdateText(){
        if(movementStateText != null){
            movementStateText.text = entityBehavior.GetEntityMovementState().ToString();
        }
        else{
            Debug.Log("no text");
        }
    }
}


