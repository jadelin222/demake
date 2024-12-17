using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterable : MonoBehaviour, IInteractable
{
    [Header("FX")]
    public ParticleSystem waterParticleEffect;
    public AudioSource audioSource;
    public AudioClip waterSound;
    public ItemType RequiredItem => ItemType.WateringCan;
    public string InteractionVerb => "Insppect";

    [SerializeField]
    private GameObject rottenObject;
    [SerializeField]
    private GameObject revivedObject;
    private bool isWatered = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        if (isWatered) return;
        if (ToolSystem.Instance.EquippedToolType == RequiredItem && !isWatered) 
        {
            PlayWaterSound();
            WaterObject();
        }
        else
        {
            UIManager.Instance.ShowBottomScreenUI("You need a Watering Can to revive this.");
        }
        
    }

    public void OnRayHit()
    {
        if (isWatered) return;
        //if they dont have the tool, prompt hint to E-inspect, bottom screen UI to find the tool, 
        if (!ToolSystem.Instance.HasTool(RequiredItem))
            UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }
    private void PlayWaterSound()
    {
        if (audioSource != null && waterSound != null)
            audioSource.PlayOneShot(waterSound);
    }
    private void WaterObject()
    {
        isWatered = true;

        //particle
        if (waterParticleEffect != null)
        {
            ParticleSystem effect = Instantiate(waterParticleEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 2f);
        }

        rottenObject.SetActive(false);
        revivedObject.SetActive(true);

    }
}
