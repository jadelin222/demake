using System.Collections;
using UnityEngine;

public class Dooor : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public bool isOpen = false;
    public bool isKeyNeeded;

    [Header("Required Item")]
    [SerializeField] 
    private ItemType requiredItem = ItemType.None; //backing field
    public ItemType RequiredItem => requiredItem;
    public string InteractionVerb => "Open";

    [Header("Animation and Sound")]
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip doorOpenSound;
    public AudioClip doorLockedSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
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
            bool hasRequiredItem = ItemSystem.Instance.HasItem(RequiredItem); ;
            //hasRequiredKey = playerInventory.HasKey(requiredKeyName);

            if (hasRequiredItem)
            {
                Debug.Log("you have the right key");
                OpenDoor();
                ItemSystem.Instance.MarkItemAsUsed(RequiredItem);
                //td:mark the key as used in inventory
            }
            else
            {
                //if no correct key obtained, display text. 
                Debug.Log("you don't seem to have the right key");
                PlayLockedAnim();
                PlayLockedSound();
            }
        }

    }

    //called when the player looks at the door
    public void OnRayHit()
    {
        Debug.Log("looking at door");
        UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("door opened");
            PlayOpenAnim();
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

    private void PlayOpenAnim()
    {
        animator.SetTrigger("Open");
    }
    private void PlayOpenSound()
    {
        //Debug.Log("playing open sound");
        audioSource.PlayOneShot(doorOpenSound);

    }
    private IEnumerator ShakeDoorRoutine()
    {
        Vector3 originalPosition = transform.localPosition;
        float duration = 1f;
        float magnitude = 0.03f;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float z = Random.Range(-magnitude, magnitude);
            transform.localPosition = originalPosition + new Vector3(0, 0, z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
    private void PlayLockedAnim()
    {
        StartCoroutine(ShakeDoorRoutine());
    }
    private void PlayLockedSound()
    {
        audioSource.PlayOneShot(doorLockedSound);
    }
    private void PlayCloseSound()
    {
        Debug.Log("play close sound");
    }
}
