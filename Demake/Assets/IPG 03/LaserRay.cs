using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LaserRay : MonoBehaviour
{
    public Transform A;
    public Transform B;

    public LineRenderer Line;
    [Range(0,1)]
    public float Width = 0.5f;

    
    // Update is called once per frame
    void Update()
    {
        Line.SetPosition(0, A.position);
        Line.SetPosition(1, (A.position + B.position) / 2f + Random.insideUnitSphere * Width);
        Line.SetPosition(2, B.position);
    }
}
