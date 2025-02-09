
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    public bool destroyAfterTrigger;
    public string storyLine;
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            UIManager.Instance.ShowBottomScreenUI(storyLine);
        if(audioSource != null)
            audioSource.Play();
        if (destroyAfterTrigger)
            Destroy(gameObject);
    }
}
