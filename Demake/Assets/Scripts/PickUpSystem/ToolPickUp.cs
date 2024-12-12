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
        if (toolObject == null)
        {
            Debug.LogError("tool object not assigned");
            return;
        }
        //display the pickup UI and pass the pickup logic as a callback
        PickupUIData uiData = new PickupUIData
        {
            displayType = PickupDisplayType.ItemOrTool,
            bottomMessage = $"Obtained {toolObject.name}",
            actionVerb = "Close"
        };

        UIManager.Instance.ShowPickupUI(uiData, FinalizePickup);
    }
    public override void FinalizePickup()
    {
        //add the tool to the ToolSystem when q presed
        if (toolObject != null)
        {
            ToolSystem.Instance.CollectTool(toolObject);
            Debug.Log($"Collected tool: {toolObject.name}");
        }
        Destroy(gameObject);
    }

}
