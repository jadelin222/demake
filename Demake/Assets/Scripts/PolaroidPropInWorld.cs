using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidPropInWorld : MonoBehaviour, IInteractable
{
    public void Interact()
    {
       //match which polaroid this is 

        Debug.Log($"Picked up polariod");
        //td: show ui
        //td: collect photo to inventory
        Destroy(gameObject);
    }

    public void OnRayHit()
    {
        Debug.Log($"Looking at polaroid");
    }
}
