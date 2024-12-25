using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digable : MonoBehaviour, IInteractable
{
    public ItemType RequiredItem => ItemType.Shovel;
    public string InteractionVerb => "Inspect";
    [Header("FX")]
    public ParticleSystem digParticleEffect;
    public float shrinkDuration = 1f; //time taken to shrink and disappear
    public AudioSource audioSource;
    public AudioClip digSound;

    private bool isDigged = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        if (isDigged) return;

        //if player have the right tool equipped, play the break obj sound and break obj
        if (ToolSystem.Instance.EquippedToolType == RequiredItem && !isDigged)
            DigStuff();
        else
            UIManager.Instance.ShowBottomScreenUI("You need a Shovel to break this.");
        
    }

    public void OnRayHit()
    {
        if (isDigged) return;
        //if they dont have the tool, prompt hint to E-inspect, bottom screen UI to find the tool, 
        if (!ToolSystem.Instance.HasTool(RequiredItem))
            UIManager.Instance.ShowControlHintUI(InteractionVerb);
    }
    private void DigStuff()
    {
        isDigged = true;

        // sound
        PlayDigSound();

        //particle
        if (digParticleEffect != null)
        {
            ParticleSystem effect = Instantiate(digParticleEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 2f); 
        }

        //shrink anim
        StartCoroutine(ShrinkAndDisappear());
    }
    private IEnumerator ShrinkAndDisappear()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;

        while (elapsedTime < shrinkDuration)
        {
            float progress = elapsedTime / shrinkDuration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero; 
        gameObject.SetActive(false);   

    }

    private void PlayDigSound()
    {
        if (audioSource != null && digSound != null)
            audioSource.PlayOneShot(digSound);
    }
}
