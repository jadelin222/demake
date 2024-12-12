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
        //UIManager.Instance.ShowPickupUI("Close", $"Obtained {toolObject.name}");
        // Display the pickup UI and pass the pickup logic as a callback
        UIManager.Instance.ShowPickupUI("Close", $"Obtained {toolObject.name}", FinalizePickup);
        //ToolSystem.Instance.CollectTool(toolObject);
        //DestroyPickup();
    }
    public override void FinalizePickup()
    {
        //add the tool to the ToolSystem when q presed
        if (toolObject != null)
        {
            ToolSystem.Instance.CollectTool(toolObject);
            Debug.Log($"Collected tool: {toolObject.name}");
        }

        DestroyPickup();
    }

}
