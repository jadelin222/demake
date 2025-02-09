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
    private List<ToolData> toolCollection = new List<ToolData>();  //metadata for UIs!
    private int currentToolIndex = 0;
    private Tool activeTool;
    private ItemType equippedItem = ItemType.None; //currently equipped item

    public ItemType EquippedToolType => equippedItem;

    //[Header("Animation Settings")]
    //public Animator toolAnimator;

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
    public void CollectTool(GameObject toolObject, Sprite toolImage, string description)
    {
        //ITool tool = toolObject.GetComponent<ITool>();
        Tool tool = toolObject.GetComponent<Tool>();

        if (tool != null && !toolInventory.Contains(tool))
        {
            toolInventory.Add(tool);
            //Debug.Log($"{toolObject.name} collected and added to inventory");
            toolCollection.Add(new ToolData(tool.toolName, toolImage, description));


            //EquipTool(currentToolIndex);
            EquipTool(toolInventory.Count - 1);
        }
    }
    public List<ToolData> GetToolCollection()
    {
        return toolCollection;
    }

    public void EquipTool(int toolIndex)
    {
        if (toolInventory.Count == 0) return;
        //StartCoroutine(SwitchTool(toolIndex));
        //toolAnimator.SetTrigger("ShowTool");

        currentToolIndex = toolIndex % toolInventory.Count;

        if (activeTool != null)
            activeTool.gameObject.SetActive(false);

        activeTool = toolInventory[currentToolIndex];
        equippedItem = activeTool.ToolType;  //update current equipped tool
        activeTool.ResetToolStatus();
        //Debug.Log($"{activeTool.ToolType} equipped");
        //show animator

    }
    //private IEnumerator SwitchTool(int toolIndex)
    //{
    //    if (activeTool != null)
    //    {
    //        toolAnimator.SetTrigger("HideTool");
    //        yield return new WaitForSeconds(0.8f); // Adjust this duration to match your hide animation length
    //        activeTool.gameObject.SetActive(false);
    //    }

    //    currentToolIndex = toolIndex % toolInventory.Count;
    //    activeTool = toolInventory[currentToolIndex];
    //    equippedItem = activeTool.ToolType;  //update current equipped tool
    //    activeTool.ResetToolStatus();
    //    Debug.Log($"{activeTool.ToolType} equipped");

    //    toolAnimator.SetTrigger("ShowTool");
    //    activeTool.gameObject.SetActive(true);
    //}
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
        else return;
    }
    public void CycleToNextTool()
    {
        if (toolInventory.Count == 0) return;
        if (activeTool != null && activeTool.IsInUse())
        {
            Debug.Log("cant cycle to next tool while the current tool is in use");
            return;
        }
        currentToolIndex = (currentToolIndex + 1) % toolInventory.Count;
        EquipTool(currentToolIndex);
        //ad animation
    }

    public void PutAwayActiveTool()
    {
        if (activeTool != null) 
        {
            //toolAnimator.SetTrigger("HideTool");
            activeTool.PutAway();
        }

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