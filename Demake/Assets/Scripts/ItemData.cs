
using UnityEngine;
[System.Serializable]
public class ItemData
{
    public Sprite imageSprite;
    public string descriptionText;
    public ItemType itemType;
    public bool isItemUsed;

    public ItemData(Sprite image, string description, ItemType type)
    {
        this.imageSprite = image;
        this.descriptionText = description;
        this.itemType = type;
        this.isItemUsed = false;
    }
}