using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class GunAdvanced : MonoBehaviour
{
    

    [Header("Bullet")]
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


    [Header("Raycast")]
    public Transform Gun;
    [Range(0, 100)]
    public float MaxDistance = 100; // m
    public Camera Camera;
    public LayerMask Layer;


    // Update is called once per frame
    void Update()
    {
        // Raycast -------------------------------
        // Rotates the gun towards the cursor
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        Vector3 endPoint;
        if (Physics.Raycast(ray, out hitInfo, MaxDistance, Layer))
            endPoint = hitInfo.point;
        else
            endPoint = FirePoint.position + ray.direction * MaxDistance;

        Gun.LookAt(endPoint);


        // -----------------------------------------
        // Every second, this variable decreases by 1
        ReloadTimer -= Time.deltaTime; // 1/fps


        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            // Not ready to shoot yet
            if (ReloadTimer > 0)
            {
                Source.PlayOneShot(CockingClip);
                return;
            }

            // Shooting
            Source.PlayOneShot(ShootingClip);
            Impulse.GenerateImpulse();

            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            ReloadTimer = ReloadTime;
        }
    }
}
