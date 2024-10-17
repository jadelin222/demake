using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public GameObject Door;

    void OnTriggerEnter(Collider collider)
    {
        Door.SetActive(false);
    }

    void OnTriggerExit(Collider collider)
    {
        Door.SetActive(true);
    }
}
