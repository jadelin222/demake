using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidPickUp : PickUp
{
    public override string InteractionVerb => "Inspect";
    public override void Interact()
    {
        Debug.Log($"picked up polaroid number x");
        //UIManager.Instance.ShowBottomScreenUI($"Picked up: {itemName}");
        UIManager.Instance.ShowPolaroidUI();

        //td: add to the polaroid inventory
        DestroyPickup();
    }

}
