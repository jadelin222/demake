using System.Collections.Generic;
using UnityEngine;

public class GrassRenderer : MonoBehaviour
{
    [Header("Grass Settings")]
    public Mesh grassMeshA;     
    public Mesh grassMeshB;        
    public Material grassMaterial;  
    public int grassCountPerSurface = 200;

    [Header("Surfaces")]
    public List<MeshFilter> spawnSurfaces; //list of surfaces to spawn grass on

    private List<Matrix4x4> matricesA = new List<Matrix4x4>();
    private List<Matrix4x4> matricesB = new List<Matrix4x4>();

    private int countA = 0;
    private int countB = 0;

    private void Start()
    {
        foreach (var surface in spawnSurfaces)
        {
            if (surface != null)
            {
                SpawnGrassOnSurface(surface);
            }
        }
    }

    private void SpawnGrassOnSurface(MeshFilter surface)
    {
        Vector3[] vertices = surface.sharedMesh.vertices;
        Transform surfaceTransform = surface.transform;

        int localCountA = 0, localCountB = 0;

        for (int i = 0; i < grassCountPerSurface; i++)
        {
            int randomIndex = Random.Range(0, vertices.Length);
            Vector3 position = surfaceTransform.TransformPoint(vertices[randomIndex]);

            //randomize posotion
            position += new Vector3(
                Random.Range(-0.1f, 0.1f),
                0f,
                Random.Range(-0.1f, 0.1f)
            );

            bool useMeshA = Random.value > 0.5f;

            if (useMeshA)
            {
                matricesA.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
                localCountA++;
            }
            else
            {
                matricesB.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
                localCountB++;
            }
        }

        countA += localCountA;
        countB += localCountB;

    }

    private void Update()
    {
        if (countA > 0)
        {
            Graphics.DrawMeshInstanced(grassMeshA, 0, grassMaterial, matricesA.ToArray(), countA);
        }

        if (countB > 0)
        {
            Graphics.DrawMeshInstanced(grassMeshB, 0, grassMaterial, matricesB.ToArray(), countB);
        }
    }

}
