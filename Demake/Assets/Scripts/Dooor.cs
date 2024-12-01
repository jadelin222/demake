using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dooor : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public bool isKeyNeeded;
    //public string requiredKeyName;
    public ItemType RequiredItem => ItemType.KeyToA;//to change to drop down menu or?

    //private PlayerInventory playerInventory;

    public void Interact()
    {
        Debug.Log("interact with door");
        //if key is not needed, open door
        if (!isKeyNeeded)
        {
            OpenDoor();
        }

        //if key needed and have key, open door, 
        //if key needed and have no key, no open, prompt text, shake door, sound fx
        else
        {
            //if need key, check if player have that key
            bool hasRequiredKey = true;
            //hasRequiredKey = playerInventory.HasKey(requiredKeyName);

            if (hasRequiredKey)
            {
                OpenDoor();

                //td:mark the key as used in inventory
            }
            else
            {
                //if no correct key obtained, display text. 
                Debug.Log("you don't seem to have the right key");
                ShakeDoor();
                PlayLockedSound();
            }
        }

    }

    //called when the player looks at the door
    public void OnRayHit()
    {
        Debug.Log("looking at door");
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("door opened");
            
            PlayOpenSound();
            
        }  
    }
    private void CloseDoor()
    {
        //or do i just let the door shut on its own
        if (isOpen)
        {
            isOpen = false;
            Debug.Log("door closed");

            PlayCloseSound();
            //td:anim door
        }
    }

    private void ShakeDoor()
    {
        //anim
        Debug.Log("door locked, shaking");
    }
    private void PlayOpenSound()
    {
        Debug.Log("playing open sound");
    }
    private void PlayCloseSound()
    {
        Debug.Log("play close sound");
    }
    private void PlayLockedSound()
    {
        Debug.Log("play locked sound");
    }
}
