using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, ITool
{
    public void UseTool()
    {
        DigThings();
    }
    private void DigThings()
    {
        Debug.Log("Plants revived!");
    }
}
