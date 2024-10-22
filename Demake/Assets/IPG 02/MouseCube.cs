using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCube : MonoBehaviour
{
    public Renderer Renderer;

    void OnMouseEnter()
    {
        Renderer.material.color = Color.red;
    }

    void OnMouseExit()
    {
        Renderer.material.color = Color.white;
    }
}
