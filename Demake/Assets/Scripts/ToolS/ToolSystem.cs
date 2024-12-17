using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//manage the overall tool inventory, which tool is currently equipped
//handle actions like equipping, unequipping, and switching between tools

public class ToolSystem : MonoBehaviour
{
    public static ToolSystem Instance;
    [SerializeField]
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
    private void Update()
    {
        //tool controls
        if (Input.GetMouseButtonDown(1))  //cycle to the next tool right mouse
            CycleToNextTool();
        if (Input.GetMouseButtonDown(0))
            UseActiveTool();
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

    public void PutAwayActiveTool()
    {
        if (activeTool != null) activeTool.PutAway();
    }

    public bool HasTool(ItemType itemType)
    {
        foreach (var tool in toolInventory)
        {
            if (tool.ToolType == itemType)
            {
                return true;
            }
        }
        return false;
    }
}