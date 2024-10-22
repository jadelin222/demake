using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    public string itemName;
    //public Sprite itemIcon;

    public abstract void Interact();

    //indicating ray hit
    public virtual void OnRayHit() 
    {
        Debug.Log($"looking at: {itemName}");
    }
}
