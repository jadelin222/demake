using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Tool
{
    public override void UseTool()
    {
        Debug.Log("hammering stuff...");
        HitStuff();
    }
    private void HitStuff()
    {
        Debug.Log("aw you are hitting on stuff");
    }
}
