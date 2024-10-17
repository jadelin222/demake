using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonEventSystem : MonoBehaviour
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
        Material = Renderer.material;
    }

    public void Click ()
    {
        On = !On;

        Material.color = On ? ColourOn : ColourOff;
        Door.SetActive(!On);
    }
}
