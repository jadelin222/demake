using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemData
{
    public Sprite imageSprite;
    public string descriptionText;
    public bool isItemUsed;

    public ItemData(Sprite image, string description)
    {
        this.imageSprite = image;
        this.descriptionText = description;
        this.isItemUsed = false;
    }
}