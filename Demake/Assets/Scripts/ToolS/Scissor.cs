using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : Tool
{
    public override void UseTool()
    {
        //Debug.Log("using scissor...");
        CutGrass();
    }
    private void CutGrass()
    {
        Debug.Log("cutting grass..");
    }
}
