using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public AudioClip breakSound;

    [SerializeField]
    private GameObject fullObject;
    [SerializeField]
    private GameObject fragments;
    public ItemType RequiredItem => ItemType.Hammer;
    public string InteractionVerb => "Inspect";

    private bool isBroken = false;  //track if the object is already broken
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        //break the item, unhide the fragments and hide full object. 
        if (isBroken) return;
        else BreakObject();

    }

    public void OnRayHit()
    { 
        //if they dont have the tool, prompt hint to E-inspect, bottom screen UI to find the tool, 
        if (!isBroken && ToolSystem.Instance.EquippedToolType == RequiredItem)
        {
            //show ui to hit with hammer
        }
    }

    private void BreakObject()
    {
        if (breakSound != null)
            audioSource.PlayOneShot(breakSound);

        isBroken = true;
        fullObject.SetActive(false);
        fragments.SetActive(true);
        //Debug.Log($"{gameObject.name} is broken!");
    }

}
