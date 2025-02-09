
using System.Collections;
using UnityEngine;

public class Trumpet : Tool
{
    [Header("Trumpet Settings")]
    //public AudioSource trumpetSound;
    public float idleDuration = 10f; //how long ghosts stay idle
    //private bool isPlaying = false;

    private void Update()
    {
        //if (Input.GetMouseButton(0)) //play when hiolding left mouse. 
        //{
        //    if (!isPlaying)
        //        ActivateTrumpet();
        //}
        //else if (isPlaying) //stop when mouse released
        //    DeactivateTrumpet();
        //if (Input.GetMouseButtonDown(0)) //play when left mouse button is pressed
        //{
        //        ActivateTrumpet();
        //}
        //if (Input.GetMouseButtonDown(0)) //play when left mouse button is pressed
        //    ActivateTool();
    }
    public override void UseTool()
    {
        StopGhosts();
    }
    private void StopGhosts()
    {
        //set all ghosts to idle
        MannequinGhostAI[] ghosts = FindObjectsOfType<MannequinGhostAI>();
        Debug.Log($"Found {ghosts.Length} ghosts.");
        foreach (var ghost in ghosts)
        {
            ghost.SetGhostStateIdle(idleDuration);
        }
    }
    //public override void ActivateTool()
    //{
    //    //cooldown check
    //    if (!CanUse())
    //        return;
    //    if (currentState == ToolState.InUse)
    //        return;

    //    PlayHitAnim();
    //    PlayHitSound();
    //    UseTool();
    //    lastUseTime = Time.time;
    //}
    //private void ActivateTrumpet()
    //{
    //    isPlaying = true;

    //    if (audioSource != null && !audioSource.isPlaying)
    //        audioSource.Play();

    //    //set all ghosts to idle
    //    MannequinGhostAI[] ghosts = FindObjectsOfType<MannequinGhostAI>();
    //    Debug.Log($"Found {ghosts.Length} ghosts.");
    //    foreach (var ghost in ghosts)
    //    {
    //        ghost.SetGhostStateIdle(idleDuration);
    //    }
    //    isPlaying = false;
    //    //StartCoroutine(ResetIsPlayingAfterSound());
    //}
    ////private IEnumerator ResetIsPlayingAfterSound()
    ////{
    ////    if (audioSource != null)
    ////    {
    ////        yield return new WaitWhile(() => audioSource.isPlaying);
    ////    }
    ////    isPlaying = false;
    ////}
    //private void DeactivateTrumpet()
    //{
    //    isPlaying = false;
    //    if (audioSource != null)
    //        audioSource.Stop();

    //}
}
