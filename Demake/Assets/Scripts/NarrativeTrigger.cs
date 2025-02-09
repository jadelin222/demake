
using System.Collections;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    public string storyLine;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowBottomScreenUI(storyLine);
            if (audioSource != null) 
            {
                audioSource.Play();
                StartCoroutine(DestroyAfterAudio());
            }
                
        }
    }

    private IEnumerator DestroyAfterAudio()
    {
        //wait until the audio has finished playing
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
