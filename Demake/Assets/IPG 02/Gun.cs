using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet BulletPrefab;
    public Transform FirePoint;

    [Header("Reload")]
    [Range(0,5)]
    public float ReloadTime = 1f; // seconds
    private float ReloadTimer = 0; // seconds

    [Header("Sound")]
    public AudioSource Source;
    public AudioClip ShootingClip;
    public AudioClip CockingClip;
    public CinemachineImpulseSource Impulse;

    [Header("Aiming")]
    [Range(0, 100)]
    public float MaxDistance = 100; // m
    public LayerMask Layer;
    public Camera Camera;

    public Transform GunObject;

    void Update()
    {
        // -------------------- AIM --------------------
        // get mouse coordinates on 2D
        // convert that into 3d
        // rotate the gun to face that point
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Vector3 hitPoint;
        if (Physics.Raycast(ray, out hitInfo, MaxDistance, Layer))
        {
            hitPoint = hitInfo.point; // the point of the raycast
        }
        else
        {
            // the end of the raycast line
            hitPoint = ray.origin + ray.direction * MaxDistance;
        }

        // Rotates the gun towards the endpoint of the raycast
        GunObject.LookAt(hitPoint);



        // -------------------- SHOOT ------------------
        // Every second, this variable decreases by 1
        ReloadTimer -= Time.deltaTime; // 1/fps

        //if (ReloadTimer <= 0 &&
        //    Input.GetMouseButtonDown(0))
        if (Input.GetMouseButtonDown(0))
        {
            // Gun ready to shoot
            if (ReloadTimer <= 0)
            {
                // shooting
                Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
                ReloadTimer = ReloadTime;

                // sound goes here
                Source.PlayOneShot(ShootingClip);

                // Screenshake
                Impulse.GenerateImpulse();
            }
            else
            {
                // Gun not ready
                Source.PlayOneShot(CockingClip);
            }
        }
    }
}
