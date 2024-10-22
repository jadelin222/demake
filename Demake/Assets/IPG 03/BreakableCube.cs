using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCube : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Fragments;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Cube.SetActive(false);
            Fragments.SetActive(true);
        }
    }
}
