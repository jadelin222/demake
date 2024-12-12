using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : PickUp
{
    public override string InteractionVerb => "Pickup";
    public override void Interact()
    {
        Debug.Log($"picked up item: {itemName}");

        UIManager.Instance.ShowPickupUI("Close", $"Obtained {itemName}", FinalizePickup);

        //td:add to the items inventory
        //DestroyPickup();
    }
    public override void FinalizePickup()
    {
        Debug.Log($"Finalizing pickup for {itemName}");

        // Add the item to the inventory
        //InventorySystem.Instance.AddItem(itemName);

        // Destroy the object in the scene
        Destroy(gameObject);
    }


}
