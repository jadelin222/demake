using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonClick : MonoBehaviour
{
    public bool On = false;

    public GameObject Door;
    public MeshRenderer Renderer;
    

    [Header("Materials")]
    public Material MaterialOff;
    public Material MaterialOn;

    void OnMouseUpAsButton()
    {
        On = ! On;
        if (On)
        {
            Door.SetActive(false);
            Renderer.sharedMaterial = MaterialOn;
        } else
        {
            Door.SetActive(true);
            Renderer.sharedMaterial = MaterialOff;
        }
    }
}
