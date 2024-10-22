using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class LaserGun_04 : MonoBehaviour
{
    [Header("Raycast")]
    public Transform Gun;
    public Transform FirePoint;
    [Range(0, 100)]
    public float MaxDistance = 100; // m
    public Camera Camera;
    public LayerMask Layer;

    [Header("Line")]
    public LineRenderer Line;
    [Range(0f, 1f)]
    public float Width = 0.1f;
    public AnimationCurve Curve;
    [Range(0f, 10f)]
    public float Speed = 1;

    public ParticleSystem Particle;


    [Header("Sound")]
    public AudioSource Audio;

    [Header("Power")]
    public float Power = 1; // Power % transmitted per second

    // Update is called once per frame
    void Update()
    {
        // Raycast --------------------------------
        // Rotates the gun towards the cursor
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        Vector3 endPoint;
        bool hit = Physics.Raycast(ray, out hitInfo, MaxDistance, Layer);
        if (hit)
            endPoint = hitInfo.point;
        else
            endPoint = FirePoint.position + ray.direction * MaxDistance;

        Gun.LookAt(endPoint);

        // Shooting ----------------------------------
        if (Input.GetMouseButton(1))
        {
            // Particles
            if (hit)
            {
                Particle.transform.position = endPoint;
                Particle.Play();

                // Check if the object hit can recieve a laser
                LaserReceiver receiver = hitInfo.collider.GetComponent<LaserReceiver>();
                if (receiver != null)
                {
                    receiver.Power(Power * Time.deltaTime);
                }
            }

            // Graphics
            Line.SetPosition(0, FirePoint.position);
            Line.SetPosition(1, (FirePoint.position + endPoint)/2f + Random.insideUnitSphere * Width/2f);
            Line.SetPosition(2, endPoint);
            Line.enabled = true;

            Line.startWidth = Width * Curve.Evaluate(Time.time * Speed);
            Line.endWidth = Width * Curve.Evaluate((Time.time + 0.5f) * Speed);

            // Sound
            Audio.UnPause();
        } else
        {
            // Particles
            Particle.Stop();

            // Graphics
            Line.enabled = false;

            // Sound
            Audio.Pause();
        }
    }
}
