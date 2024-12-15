using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : Tool
{
    [Range(0f, 2f)]
    public float cutRadius = 1f;
    public override void UseTool()
    {
        CutGrass();
    }
    private void CutGrass()
    {
        GrassRenderer grassRenderer = FindObjectOfType<GrassRenderer>();
        if (grassRenderer != null)
        {
            Vector3 cutPosition = transform.position; 
            grassRenderer.CutGrass(cutPosition, cutRadius);
        }
    }
}
