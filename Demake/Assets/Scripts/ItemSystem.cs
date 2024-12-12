using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    public static ItemSystem Instance;

    private List<ItemData> itemCollection = new List<ItemData>();

    private void Awake()
    {
        Instance = this;
    }
    public void CollectItem(Sprite image, string description)
    {
        ItemData newItem = new ItemData(image, description);
        itemCollection.Add(newItem);
        Debug.Log($"Collected Item: {description}");
    }
    public List<ItemData> GetItemCollection()
    {
        return itemCollection;
    }
}
