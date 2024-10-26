using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//manage the overall tool inventory, which tool is currently equipped
//handle actions like equipping, unequipping, and switching between tools
public enum ToolType
{
    WateringCan,
    Scissors,
    Shovel,
    Trumpet
}
public class ToolSystem : MonoBehaviour
{
    public static ToolSystem Instance;

    private List<ITool> toolInventory = new List<ITool>(); 
    private int currentToolIndex = 0; 
    private ITool activeTool;

    void Awake()
    {
        Instance = this;
    }

    public void CollectTool(GameObject toolObject)
    {
        ITool tool = toolObject.GetComponent<ITool>();

        if (tool != null && !toolInventory.Contains(tool))
        {
            toolInventory.Add(tool);  
            Debug.Log($"{toolObject.name} collected and added to inventory.");
            EquipTool(currentToolIndex);
        }
    }

    public void EquipTool(int toolIndex)
    {
        if (toolInventory.Count == 0) return; 

        currentToolIndex = toolIndex % toolInventory.Count;
        if (activeTool != null)
        {
            (activeTool as MonoBehaviour).gameObject.SetActive(false);
        }

        activeTool = toolInventory[currentToolIndex];
        (activeTool as MonoBehaviour).gameObject.SetActive(true); 

        Debug.Log($"{(activeTool as MonoBehaviour).name} equipped");
    }

    public void UseActiveTool()
    {
        if (activeTool != null)
        {
            activeTool.UseTool();
        }
        else
        {
            Debug.Log("no tool equipped.");
        }
    }

    public void CycleToNextTool()
    {
        if (toolInventory.Count == 0) return;
        currentToolIndex = (currentToolIndex + 1) % toolInventory.Count;
        EquipTool(currentToolIndex);
    }
}