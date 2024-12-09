using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : PickUp
{
    public override void Interact()
    {
        Debug.Log($"picked up item: {itemName}");
        //UIManager.Instance.ShowBottomScreenUI($"Picked up: {itemName}");

        //td:add to the items inventory
        DestroyPickup();
    }

   
}
