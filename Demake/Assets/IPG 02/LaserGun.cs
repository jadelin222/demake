using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public Transform FirePoint;
    [Range(0,100)]
    public float MaxDistance = 100; // m

    public LayerMask Layer;

    public Camera Camera;

    [Header("Sound")]
    public AudioSource Source;

    public LineRenderer Line;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Vector3 hitPoint;
            //if (Physics.Raycast(FirePoint.position, FirePoint.forward, out hitInfo, MaxDistance, Layer))
            if (Physics.Raycast(ray, out hitInfo, MaxDistance, Layer))
            {
                //Debug.DrawLine(FirePoint.position, hitInfo.point, Color.red);
                hitPoint = hitInfo.point;
            }
            else
            {
                hitPoint = ray.origin + ray.direction * MaxDistance;
            }

            // Play sound here
            Source.UnPause();

            // Line
            Line.SetPosition(0, FirePoint.position);
            Line.SetPosition(1, (FirePoint.position + hitPoint)/2f
                + Random.insideUnitSphere*0.1f);
            Line.SetPosition(2, hitPoint); 
            Line.enabled = true;
        }
        else
        {
            Source.Pause();

            Line.enabled = false;
        }
    }
}
