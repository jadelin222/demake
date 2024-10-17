using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IPG.Lecture03
{
    public class Door : MonoBehaviour
    {
        [Header("Door")]
        public Transform DoorObject;
        public Transform Transform0;
        public Transform Transform1;

        [Header("Input")]
        public LaserReceiver Receiver;
        [Range(0f, 1f)]
        public float Min;
        [Range(0f, 1f)]
        public float Max;

        private void Update()
        {
            // Receiver.t: [0, 0.5, 0.75, 1]
            // t:          [0, 0,   1,    1]
            float t = Mathf.InverseLerp(Min, Max, Receiver.T);
            DoorObject.position = Vector3.Lerp(Transform0.position, Transform1.position, t);
            //DoorObject.position = Vector3.Lerp(Transform0.position, Transform1.position, Receiver.T);
        }
    }
}