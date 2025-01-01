
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    public string storyLine;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the trigger
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowBottomScreenUI(storyLine);
        }
    }
}
