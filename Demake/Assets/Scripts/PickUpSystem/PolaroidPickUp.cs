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
        //UIManager.Instance.ShowPolaroidUI();
        PickupUIData uiData = new PickupUIData
        {
            displayType = PickupDisplayType.Polaroid,
            bottomMessage = $"You found a Polaroid!",
            descriptionText = descriptionText,
            imageSprite = polaroidImage,
            actionVerb = "OK"
        };

        UIManager.Instance.ShowPickupUI(uiData, FinalizePickup);

    }
    //public override void FinalizePickup()
    //{
    //    //td: add to the polaroid inventory

    //    DestroyPickup();
    //}

}
