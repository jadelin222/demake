using UnityEngine;

[System.Serializable]
public class PickupData
{
    public PickupDisplayType displayType; 
    public string bottomMessage; //"obtained xxx"
    public string actionVerb; //"Q - Collect"
    public string descriptionText; //letters or polaroid descriptions
    public Sprite imageSprite; // img for polaroids

    public PickupData(PickupDisplayType displayType, string actionVerb, string bottomMessage, string descriptionText, Sprite imageSprite)
    {
        this.displayType = displayType;
        this.actionVerb = actionVerb;
        this.bottomMessage = bottomMessage;
        this.descriptionText = descriptionText;
        this.imageSprite = imageSprite;
    }
}