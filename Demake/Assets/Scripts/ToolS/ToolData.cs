using UnityEngine;

[System.Serializable]
public class ToolData
{
    public string toolName;   
    public Sprite imageSprite;  
    public string descriptionText;  
    public ToolData(string name, Sprite image, string description)
    {
        toolName = name;
        imageSprite = image;
        descriptionText = description;
    }
}