using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a prop in the game to pickup, it is an interactable, when picked up, add the corresponding object to the inventory.
public class ToolPropInWorld : MonoBehaviour, IInteractable
{
    public GameObject toolObject;
    public ItemType RequiredItem => ItemType.None;
    public string InteractionVerb => "Pickup";
    public void Interact()
    {
        if (toolObject == null)
        {
            Debug.LogError("Tool object is not assigned in ToolPropInWorld.");
            return;
        }

        Debug.Log($"Picked up {toolObject.name}");

        ToolSystem.Instance.CollectTool(toolObject);
        Destroy(gameObject); 
    }

    public void OnRayHit()
    {
        Debug.Log($"Looking at {toolObject.name}");
        UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }

}