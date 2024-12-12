using UnityEngine;

[System.Serializable]
public class PickupUIData
{
    public PickupDisplayType displayType; 
    public string bottomMessage; //"obtained xxx"
    public string actionVerb; //"Q - Collect"
    public string descriptionText; //letters or polaroid descriptions
    public Sprite imageSprite; // img for polaroids
}