using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPickUP : PickUp
{
    public override string InteractionVerb => "Inspect";
    public override void Interact()
    {
        Debug.Log($"looking at note x");
        PickupUIData uiData = new PickupUIData
        {
            displayType = PickupDisplayType.Letter,
            bottomMessage = $"{itemName}",
            descriptionText = descriptionText,
            actionVerb = "Close"
        };

        UIManager.Instance.ShowPickupUI(uiData, FinalizePickup);

    }
    public override void FinalizePickup()
    {
        return;
    }

}
