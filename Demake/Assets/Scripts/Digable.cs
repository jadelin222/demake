using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digable : MonoBehaviour, IInteractable
{
    
    public ItemType RequiredItem => ItemType.Shovel;
    public string InteractionVerb => "Dig";
    [Header("FX")]
    public ParticleSystem digParticleEffect;
    public float shrinkDuration = 1f; //time taken to shrink and disappear
    public AudioClip digSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        if (ToolSystem.Instance.EquippedToolType == RequiredItem)
        {
            DigStuff();
        }
        else
        {
            Debug.Log($"you need a {RequiredItem} equipped to dig this!!!");
        }
    }

    public void OnRayHit()
    {
        return;
    }
    private void DigStuff()
    {
        //particle
        if (digParticleEffect != null)
        {
            ParticleSystem effect = Instantiate(digParticleEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, 2f); 
        }

       // sound
        if (digSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(digSound);
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

        Debug.Log("dirt pile removed!");
    }
}
