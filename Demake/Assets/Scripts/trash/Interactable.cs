using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ToolType
//{
//    None,
//    Shovel,
//    Hammer,
//    WateringCan,
//    Trumpet

//}

public abstract class Interactable : MonoBehaviour
{
    public bool isCollectable;
    public bool haveMultipleLines;
    public string ControlHint { get; private set; }
    public string ItemDescription { get; private set; }
    public abstract void Interact();   //pick, open, close, etc
    //public abstract void OnRayHit();
    public virtual void OnRayHit()
    {
        GetControlHint();
        GetItemDescription();
    }

    public void GetControlHint()
    {
        //td: wwhen looking at the 
    }
    public void GetItemDescription()
    {
        
    }
    public void GetMissingInteractionItemHint()
    {

    }


}
