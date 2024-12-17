using System.Collections.Generic;
using UnityEngine;

public class GhostActivator : MonoBehaviour
{
    [Header("Ghosts to Activate")]
    public List<MannequinGhostAI> ghosts = new List<MannequinGhostAI>(); // List of ghosts to activate

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            foreach (var ghost in ghosts) 
            {
                ghost.ActivateGhost();
            }     
        }
    }
}
