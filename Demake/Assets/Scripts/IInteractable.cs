using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    ItemType RequiredItem { get; }
    void Interact();   //pick, open, close, etc
    void OnRayHit(); //show ui, sound etc
    //string GetInteractionPrompt();

}
