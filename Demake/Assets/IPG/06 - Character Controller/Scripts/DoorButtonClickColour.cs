using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonClickColour : MonoBehaviour
{
    public bool On = false;

    public GameObject Door;
    public MeshRenderer Renderer;
    

    [Header("Colours")]
    public Color ColourOff;
    public Color ColourOn;
    private Material Material;

    void Start()
    {
        //Material = new Material(Renderer.sharedMaterial);
        //Renderer.material = Material;

        Material = Renderer.material;
    }

    void OnMouseUpAsButton()
    {
        On = ! On;

        Material.color = On ? ColourOn : ColourOff;
        Door.SetActive(!On);
    }
}
