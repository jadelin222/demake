using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterable : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    public AudioClip waterSound;
    public ItemType RequiredItem => ItemType.WateringCan;
    public string InteractionVerb => "Insppect";

    [SerializeField]
    private GameObject rottenObject;
    [SerializeField]
    private GameObject revivedObject;
    private bool isWatered = false;
    public void Interact()
    {
        if (isWatered) return;
        PlayWaterSound();
        WaterObject();
    }

    public void OnRayHit()
    {
        return;
    }
    private void PlayWaterSound()
    {
        if (audioSource != null && waterSound != null)
            audioSource.PlayOneShot(waterSound);
    }
    private void WaterObject()
    {
        isWatered = true;
        rottenObject.SetActive(false);
        revivedObject.SetActive(true);

    }
}
