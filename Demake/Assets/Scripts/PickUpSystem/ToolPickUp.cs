using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//previously used ToolPropInWorld.cs to pick up tool objects
//a prop in the game to pickup, it is an interactable, when picked up, add the corresponding object to the inventory.
public class ToolPickUp : PickUp
{
    public GameObject toolObject;
    public override void Interact()
    {
        if (toolObject == null)
        {
            Debug.LogError("tool object not assigned");
            return;
        }

        Debug.Log($"Picked up tool: {toolObject.name}");
        ToolSystem.Instance.CollectTool(toolObject);
        DestroyPickup();
    }

}
