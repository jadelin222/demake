using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
    public GameObject fullObject;
    public GameObject fragments;
    public ItemType RequiredItem => ItemType.Hammer;
    public string InteractionVerb => "Break";

    private bool isBroken = false;  // To track if the object is already broken
    public void Interact()
    {
        //break the item, unhide the fragments and hide full object. 
        if (isBroken) return;
        else BreakObject();

    }

    public void OnRayHit()
    { 
        if (!isBroken && ToolSystem.Instance.EquippedToolType == RequiredItem)
        {
            //show ui to hit with hammer
        }
    }

    private void BreakObject()
    {
        isBroken = true;
        fullObject.SetActive(false);
        fragments.SetActive(true);
        //Debug.Log($"{gameObject.name} is broken!");
    }

}
