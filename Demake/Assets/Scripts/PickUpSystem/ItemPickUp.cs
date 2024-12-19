
using UnityEngine;

public class ItemPickUp : PickUp
{
    public ItemType itemType;
    public override string InteractionVerb => "Pickup";
    public override void Interact()
    {
        //Debug.Log($"picked up item: {itemName}");

        var uiData = new PickupData
           (PickupDisplayType.ItemOrTool,
           "OK",
           $"Collected {itemName}",
           descriptionText,
           image);

        UIManager.Instance.ShowPickupUI(uiData, FinalizePickup);

    }
    public override void FinalizePickup()
    {
        ItemData itemData = new ItemData(image, descriptionText, itemType);
        InventoryManager.Instance.AddItem(itemData);
        Destroy(gameObject);
    }


}
