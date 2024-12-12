
using UnityEngine;

public class ItemPickUp : PickUp
{
    public override string InteractionVerb => "Pickup";
    public override void Interact()
    {
        //Debug.Log($"picked up item: {itemName}");

        PickupUIData uiData = new PickupUIData
        {
            displayType = PickupDisplayType.ItemOrTool,
            bottomMessage = $"Obtained {itemName}",
            actionVerb = "Close"
        };

        UIManager.Instance.ShowPickupUI(uiData, FinalizePickup);
        //td:add to the items inventory
        //DestroyPickup();
    }
    public override void FinalizePickup()
    {

        // Add the item to the inventory
        //InventorySystem.Instance.AddItem(itemName);
        Destroy(gameObject);
    }


}
