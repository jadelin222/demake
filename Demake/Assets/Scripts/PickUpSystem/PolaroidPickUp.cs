using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PolaroidPickUp : PickUp
{
    public override string InteractionVerb => "Inspect";
    public override void Interact()
    {
        //Debug.Log($"picked up polaroid number x");

        PickupData uiData = new PickupData
            (PickupDisplayType.Polaroid,
            "OK",
            $"Collected {itemName}",
            descriptionText,
            image);

        UIManager.Instance.ShowPickupUI(uiData, FinalizePickup);


    }
    public override void FinalizePickup()
    {
        //PolaroidSystem.Instance.CollectPolaroid(image, descriptionText);
        PolaroidData polaroidData = new PolaroidData(image, descriptionText);
        InventoryManager.Instance.AddPolaroid(polaroidData);
        Destroy(gameObject);
    }

}
