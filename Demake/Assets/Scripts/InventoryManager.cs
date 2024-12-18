using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<PolaroidData> polaroidCollection = new List<PolaroidData>();
    private List<ItemData> itemCollection = new List<ItemData>();
    private List<ToolData> toolCollection = new List<ToolData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPolaroid(PolaroidData polaroid)
    {
        polaroidCollection.Add(polaroid);
    }

    public void AddItem(ItemData item)
    {
        itemCollection.Add(item);
    }

    public void AddTool(ToolData tool)
    {
        toolCollection.Add(tool);
    }

    public int GetCategoryCount(InventoryCategory category)
    {
        switch (category)
        {
            case InventoryCategory.Polaroids: 
                return polaroidCollection.Count;
            case InventoryCategory.Items: 
                return itemCollection.Count;
            case InventoryCategory.Tools: 
                return toolCollection.Count;
            default: return 0;
        }
    }

    public List<string> GetNames(InventoryCategory category)
    {
        List<string> names = new List<string>();

        switch (category)
        {
            case InventoryCategory.Polaroids:
                foreach (var p in polaroidCollection) 
                    names.Add(p.descriptionText);
                break;
            case InventoryCategory.Items:
                foreach (var i in itemCollection) 
                    names.Add(i.itemType.ToString());
                break;
            case InventoryCategory.Tools:
                foreach (var t in toolCollection) 
                    names.Add(t.toolName);
                break;
        }

        return names;
    }

    public object GetMetaData(InventoryCategory category, int index)
    {
        switch (category)
        {
            case InventoryCategory.Polaroids:
                return polaroidCollection[index];
            case InventoryCategory.Items:
                return itemCollection[index];
            case InventoryCategory.Tools:
                return toolCollection[index];
            default:
                return null;
        }
    }

    //public bool InventoryEmpty()
    //{

    //}
}
