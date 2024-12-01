using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//manage the overall tool inventory, which tool is currently equipped
//handle actions like equipping, unequipping, and switching between tools

public class ToolSystem : MonoBehaviour
{
    public static ToolSystem Instance;

    private List<Tool> toolInventory = new List<Tool>();
    private int currentToolIndex = 0;
    //private ITool activeTool;
    private Tool activeTool;
    private ItemType equippedItem = ItemType.None; //currently equipped item

    public ItemType EquippedToolType => equippedItem;
    void Awake()
    {
        Instance = this;
    }
    public void CollectTool(GameObject toolObject)
    {
        //ITool tool = toolObject.GetComponent<ITool>();
        Tool tool = toolObject.GetComponent<Tool>();

        if (tool != null && !toolInventory.Contains(tool))
        {
            toolInventory.Add(tool);  
            Debug.Log($"{toolObject.name} collected and added to inventory");
            //EquipTool(currentToolIndex);
            EquipTool(toolInventory.Count - 1);
        }
    }

    public void EquipTool(int toolIndex)
    {
        if (toolInventory.Count == 0) return; 

        currentToolIndex = toolIndex % toolInventory.Count;

        if (activeTool != null)
        {
            activeTool.gameObject.SetActive(false);
        }

        activeTool = toolInventory[currentToolIndex];
        equippedItem = activeTool.ToolType;  //update current equipped tool
        activeTool.ResetToolStatus();
        Debug.Log($"{activeTool.ToolType} equipped");
        //Debug.Log($"{activeTool.name} equipped");
    }
    public void UseActiveTool()
    {
        //if (interactable == null)
        //{
        //    Debug.Log("no interactable object detected");
        //    return;
        //}

        //ItemType requiredItem = interactable.RequiredItem;

        ////case 1:equipped tool = required item
        //if (ToolSystem.Instance.EquippedToolType == requiredItem)
        //{
        //    Debug.Log($"Using equipped tool ({ToolSystem.Instance.EquippedToolType}) on {interactable}.");
        //    activeTool?.ActivateTool(); // Perform tool action
        //    interactable.Interact();   // Trigger interactable action
        //}
        ////case 2: the interactable only requires possession of the item
        //else if (ToolSystem.Instance.HasItem(requiredItem))
        //{
        //    Debug.Log($"Using possessed item ({requiredItem}) on {interactable}.");
        //    interactable.Interact(); // Perform interactable action
        //}
        //// case 3: Neither equipped nor possessed item matches the required item
        //else
        //{
        //    Debug.Log($"You need a {requiredItem} to interact with this.");
        //    //td ui prompt
        //}


        if (activeTool != null)
        {
            if (!activeTool.gameObject.activeSelf)
            {
                activeTool.ResetToolStatus(); // show and reset the tool if it was put away
            }
            activeTool.ActivateTool();
            //activeTool.UseTool();
        }
        else
        {
            Debug.Log("No tool equipped");
            return;
        }
    }
    public void CycleToNextTool()
    {
        if (toolInventory.Count == 0) return;
        currentToolIndex = (currentToolIndex + 1) % toolInventory.Count;
        EquipTool(currentToolIndex);
    }
}