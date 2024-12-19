using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//previously used ToolPropInWorld.cs to pick up tool objects
//a prop in the game to pickup, it is an interactable, when picked up, add the corresponding object to the inventory.
public class ToolPickUp : PickUp
{
    public GameObject toolObject;
    public override string InteractionVerb => "Pickup";
    public override void Interact()
    {
        if (toolObject == null) return;

        //display the pickup UI and pass the pickup logic as a callback
        var uiData = new PickupData
            (PickupDisplayType.ItemOrTool, 
            "Close", 
            $"Obtained {toolObject.name}", 
            "",
            image);
        UIManager.Instance.ShowPickupUI(uiData, FinalizePickup);

    }
    public override void FinalizePickup()
    {
        //add the tool to the ToolSystem when q presed
        if (toolObject != null)
        {
            ToolSystem.Instance.CollectTool(toolObject, image, descriptionText);
            ToolData toolData = new ToolData(toolObject.name, image, descriptionText);
            InventoryManager.Instance.AddTool(toolData);
        }
        Destroy(gameObject);
    }

}
