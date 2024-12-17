using UnityEngine;

public class Breakable : MonoBehaviour, IInteractable
{
    [Header("FX")]
    public ParticleSystem breakParticleEffect;
    public AudioSource audioSource;
    public AudioClip breakSound;      
    //public AudioClip failedSound;

    [Header("Object References")]
    [SerializeField]
    private GameObject fullObject;
    [SerializeField]
    private GameObject fragments;

    public ItemType RequiredItem => ItemType.Hammer; //required tool
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

        //if player have the right tool equipped, play the break obj sound and break obj
        if (ToolSystem.Instance.EquippedToolType == RequiredItem && !isBroken)
            BreakObject();
        else if (ToolSystem.Instance.EquippedToolType != RequiredItem && !isBroken)
        {
            //PlayFailedInteractionSoundFX();
            UIManager.Instance.ShowBottomScreenUI("You need a hammer to break this.");
        }
        //if player dont have required tool, show btm screen ui to hint they need that tool
        else if (!ToolSystem.Instance.HasTool(RequiredItem))
            UIManager.Instance.ShowBottomScreenUI("You need a hammer to break this.");

    }

    public void OnRayHit()
    {
        if (isBroken) return;
        //if they dont have the tool, prompt hint to E-inspect, bottom screen UI to find the tool, 
        if (!ToolSystem.Instance.HasTool(RequiredItem))
            UIManager.Instance.ShowControlHintUI(InteractionVerb);  
    }

    private void BreakObject()
    {
        if (breakSound != null)
            audioSource.PlayOneShot(breakSound);

        isBroken = true;

        //particle
        if (breakParticleEffect != null)
        {
            ParticleSystem effect = Instantiate(breakParticleEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 2f);
        }
        fullObject.SetActive(false);
        fragments.SetActive(true);
        //Debug.Log($"{gameObject.name} is broken!");
    }
    //private void PlayFailedInteractionSoundFX()
    //{
    //    if (failedSound != null)
    //        audioSource.PlayOneShot(failedSound);
    //}

}
