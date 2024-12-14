using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerRenderer : MonoBehaviour
{
    [Header("Flower Settings")]
    public Mesh flowerMesh;       
    public Material flowerMaterial; 
    public int flowerCountPerSurface = 50;

    [Header("Surfaces")]
    public List<MeshFilter> spawnSurfaces; 

    private List<Matrix4x4> flowerMatrices = new List<Matrix4x4>();
    private int flowerCount = 0;

    private void Start()
    {
        foreach (var surface in spawnSurfaces)
        {
            if (surface != null)
            {
                SpawnFlowersOnSurface(surface);
            }
        }
    }

    private void SpawnFlowersOnSurface(MeshFilter surface)
    {
        Vector3[] vertices = surface.sharedMesh.vertices;
        Transform surfaceTransform = surface.transform;

        int localCount = 0;

        for (int i = 0; i < flowerCountPerSurface; i++)
        {
            int randomIndex = Random.Range(0, vertices.Length);
            Vector3 position = surfaceTransform.TransformPoint(vertices[randomIndex]);

            position += new Vector3(
                Random.Range(-0.1f, 0.1f),
                0f,
                Random.Range(-0.1f, 0.1f)
            );

            flowerMatrices.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
            localCount++;
        }

        flowerCount += localCount;

    }

    private void Update()
    {
        if (flowerCount > 0)
        {
            Graphics.DrawMeshInstanced(flowerMesh, 0, flowerMaterial, flowerMatrices.ToArray(), flowerCount);
        }
    }
}
