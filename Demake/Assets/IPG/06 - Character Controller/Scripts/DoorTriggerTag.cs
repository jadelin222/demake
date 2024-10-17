using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerTag : MonoBehaviour
{
    public GameObject Door;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            Door.SetActive(false);
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
            Door.SetActive(true);
    }
}
