using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignDropEvent : MonoBehaviour
{
    public GameObject sign;
    public string storyLine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger.");
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            UIManager.Instance.ShowBottomScreenUI(storyLine);
            Rigidbody signRigidbody = sign.GetComponent<Rigidbody>();

            signRigidbody.useGravity = true;
            signRigidbody.isKinematic = false;

            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
