using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IPG.Lecture03
{
    public class DoorThreshold : MonoBehaviour
    {
        [Header("Door")]
        public GameObject DoorObject;

        [Header("Input")]
        public LaserReceiver Receiver;
        [Range(0f, 1f)]
        public float Threshold;

        void Update()
        {
            DoorObject.SetActive(Receiver.T < Threshold);

            /*
            if (Receiver.T >= Threshold)
                DoorObject.SetActive(true);
            else
                DoorObject.SetActive(false);
            */
        }
    }
}