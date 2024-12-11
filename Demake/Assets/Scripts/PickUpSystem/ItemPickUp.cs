using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : PickUp
{
    public override string InteractionVerb => "Pickup";
    public override void Interact()
    {
        Debug.Log($"picked up item: {itemName}");

        UIManager.Instance.ShowPickupUI("Close", $"Obtained {itemName}");

        //td:add to the items inventory
        DestroyPickup();
    }

   
}
