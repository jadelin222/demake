using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : Tool
{  
    [Range(0f, 2f)]
    public float cutRadius = 1.5f;

    [Header("Cut Point Reference")]
    public Transform cutTransformPoint;
    private void OnDrawGizmos() //debug visualize cut area
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Vector3 cutPosition = cutTransformPoint.position;

        Gizmos.DrawWireSphere(cutPosition, cutRadius);
    }
    public override void UseTool()
    {
        CutGrass();    
    }
    private void CutGrass()
    {
        GrassRenderer grassRenderer = FindObjectOfType<GrassRenderer>();
        if (grassRenderer != null)
        {
            Vector3 cutPosition = cutTransformPoint.position - Vector3.up*0.5f; //make cut position lower!
            grassRenderer.CutGrass(cutPosition, cutRadius);
        }
    }
}
