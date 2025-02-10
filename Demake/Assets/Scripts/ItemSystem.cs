using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    public static ItemSystem Instance;
    [SerializeField]
    private List<ItemData> itemCollection = new List<ItemData>();

    private void Awake()
    {
        Instance = this;
    }
    public void CollectItem(Sprite image, string description, ItemType type)
    {
        ItemData newItem = new ItemData(image, description, type);
        itemCollection.Add(newItem);
        //Debug.Log($"Collected Item: {description}");
    }
    public List<ItemData> GetItemCollection()
    {
        return itemCollection;
    }
    //check if the player has the required item
    public bool HasItem(ItemType itemType)
    {
        foreach (var item in itemCollection)
        {
            if (item.itemType == itemType && !item.isItemUsed)
            {
                return true;
            }
        }
        return false;
    }
    public void MarkItemAsUsed(ItemType usedType)
    {
        var item = itemCollection.Find(i => i.itemType == usedType && !i.isItemUsed);
        if (item != null)
            item.isItemUsed = true;
        else
            Debug.LogWarning($"no usable item of type {usedType} found.");
    }
}
