using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterable : MonoBehaviour, IInteractable
{ 
    public ItemType RequiredItem => ItemType.WateringCan;
    public string InteractionVerb => "Water";

    [SerializeField]
    private GameObject rottenObject;
    [SerializeField]
    private GameObject revivedObject;
    private bool isWatered = false;
    public void Interact()
    {
        if (isWatered) return;
        else WaterObject();
    }

    public void OnRayHit()
    {
        return;
    }
    private void WaterObject()
    {
        Debug.Log("Revived!");
        isWatered = true;
        rottenObject.SetActive(false);
        revivedObject.SetActive(true);

    }
}
