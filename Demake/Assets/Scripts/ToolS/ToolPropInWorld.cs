using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPropInWorld : MonoBehaviour, IInteractable
{
    //public ToolType toolType; 
    public GameObject toolObject;
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
    }

}