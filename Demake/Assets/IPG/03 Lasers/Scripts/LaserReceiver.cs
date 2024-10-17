using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IPG.Lecture03
{
    public class LaserReceiver : MonoBehaviour
    {
        [Header("Render")]
        public Renderer Renderer;
        [ColorUsage(false, true)]
        public Color ColorOn;
        public Color ColorOff;

        [Header("Power")]
        [Range(0f, 1f)]
        public float T = 0;

        [Range(0f, 1f)]
        public float Decay = 0.1f; // Per second

        public void Power(float t)
        {
            T += t;
            T = Mathf.Clamp01(T);
        }

        // Updates the colour every frame
        void Update()
        {
            // Updates the colour
            Color color = Color.Lerp(ColorOff, ColorOn, T);
            Renderer.material.SetColor("_EmissionColor", color);

            // Power decay
            T -= Decay * Time.deltaTime;
            T = Mathf.Clamp01(T);
        }
    }
}