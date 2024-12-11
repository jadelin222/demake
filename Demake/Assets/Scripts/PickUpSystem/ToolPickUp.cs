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

        Debug.Log($"Picked up tool: {toolObject.name}");
        // Display the pickup UI using the UIManager
        UIManager.Instance.ShowPickupUI("Close", $"Obtained {toolObject.name}");
        ToolSystem.Instance.CollectTool(toolObject);
        DestroyPickup();
    }

}
