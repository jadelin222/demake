
using UnityEngine;

public class Trumpet : Tool
{
    [Header("Trumpet Settings")]
    public AudioSource trumpetSound; // Assign trumpet sound
    public float idleDuration = 10f; // How long ghosts stay idle
    private bool isPlaying = false;

    private void Update()
    {
        if (Input.GetMouseButton(0)) //play when hiolding left mouse. 
        {
            if (!isPlaying)
                ActivateTrumpet();
        }
        else if (isPlaying) //stop when mouse released
            DeactivateTrumpet();
    }
    public override void UseTool()
    {
    }
    private void ActivateTrumpet()
    {
        isPlaying = true;

        if (trumpetSound != null && !trumpetSound.isPlaying)
            trumpetSound.Play();

        //set all ghosts to idle
        MannequinGhostAI[] ghosts = FindObjectsOfType<MannequinGhostAI>();
        Debug.Log($"Found {ghosts.Length} ghosts.");
        foreach (var ghost in ghosts)
        {
            ghost.SetGhostStateIdle(idleDuration);
        }
    }

    private void DeactivateTrumpet()
    {
        isPlaying = false;
        if (trumpetSound != null)
            trumpetSound.Stop();

    }
}
