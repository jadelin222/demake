using UnityEngine;

[System.Serializable]
public class PolaroidData
{
    //public string polaroidName;
    public Sprite imageSprite;
    public string descriptionText;
    public bool isPuzzleSolved;

    public PolaroidData(Sprite image, string description)
    {
        //this.polaroidName = name;
        this.imageSprite = image;
        this.descriptionText = description;
        this.isPuzzleSolved = false;
    }
}