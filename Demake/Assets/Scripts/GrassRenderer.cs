using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SurfaceSettings
{
    public MeshFilter surface; // spawn surface
    public bool spawnGrass = true;   
    public bool spawnFlowers = false;  
}
public class GrassRenderer : MonoBehaviour
{
    [Header("Grass Settings")]
    public Mesh grassMeshA;     
    public Mesh grassMeshB;        
    public Material grassMaterial;  
    public int grassCountPerSurface = 200;

    [Header("Flower Settings")]
    public Mesh flowerMesh;
    public Material flowerMaterial;
    public int flowerCountPerSurface = 50;

    [Header("Surfaces")]
    public List<SurfaceSettings> spawnSurfaces; //list of surfaces to spawn grass/grass+flowers on

    private List<Matrix4x4> matricesA = new List<Matrix4x4>(); //grass
    private List<Matrix4x4> matricesB = new List<Matrix4x4>(); //grass
    private List<Matrix4x4> matricesFlower = new List<Matrix4x4>();

    private List<Vector3> placedPositions = new List<Vector3>(); 

    private float minDistance = 0.4f;

    //private int countA = 0;
    //private int countB = 0;

    private void Start()
    {
        foreach (SurfaceSettings surfaceSettings in spawnSurfaces)
        {
            if (surfaceSettings.surface != null)
            {
                if (surfaceSettings.spawnGrass)
                    SpawnGrassOnSurface(surfaceSettings.surface);
                if (surfaceSettings.spawnFlowers)
                    SpawnFlowersOnSurface(surfaceSettings.surface);
            }
        }
    }
    private void SpawnGrassOnSurface(MeshFilter surface)
    {
        SpawnInstances(surface, grassCountPerSurface, grassMeshA, grassMeshB, matricesA, matricesB);
    }

    private void SpawnFlowersOnSurface(MeshFilter surface)
    {
        SpawnInstances(surface, flowerCountPerSurface, flowerMesh, null, matricesFlower, null);
    }
    private void SpawnInstances(MeshFilter surface, int instanceCount, Mesh meshA, Mesh meshB, List<Matrix4x4> matricesA, List<Matrix4x4> matricesB)
    {
        Vector3[] vertices = surface.sharedMesh.vertices;
        Transform surfaceTransform = surface.transform;

        for (int i = 0; i < instanceCount; i++)
        {
            bool positionFound = false;
            Vector3 position = Vector3.zero;

            for (int attempt = 0; attempt < 10; attempt++) //reetry finding a valid position
            {
                int randomIndex = Random.Range(0, vertices.Length);
                position = surfaceTransform.TransformPoint(vertices[randomIndex]);

                //position random offset
                position += new Vector3(
                    Random.Range(-0.2f, 0.2f),
                    0f,
                    Random.Range(-0.2f, 0.2f)
                );

                //check overlap
                if (!IsTooClose(position, placedPositions))
                {
                    positionFound = true;
                    placedPositions.Add(position); //add position globally
                    break;
                }
            }

            if (!positionFound) continue;

            float verticalOffset = 0.01f; //make the grass sink inthe floor
            position.y -= verticalOffset;
            //place the instance in the appropriate list
            if (meshB != null) //grass
            {
                bool useMeshA = Random.value > 0.5f;  //50%chance spawning each grass mesh
                if (useMeshA)
                    matricesA.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
                else
                    matricesB.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
            }
            else if (matricesB == null) // flowers
                matricesFlower.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
        }
    }

    private bool IsTooClose(Vector3 position, List<Vector3> positions)
    {
        foreach (Vector3 placedPosition in positions)
        {
            if (Vector3.Distance(position, placedPosition) < minDistance)
                return true;
        }
        return false;
    }

    private void Update()
    {
        //grass A
        if (matricesA.Count > 0)
            Graphics.DrawMeshInstanced(grassMeshA, 0, grassMaterial, matricesA.ToArray());
        //grass B
        if (matricesB.Count > 0)
            Graphics.DrawMeshInstanced(grassMeshB, 0, grassMaterial, matricesB.ToArray());

        //flowers
        if (matricesFlower.Count > 0)
            Graphics.DrawMeshInstanced(flowerMesh, 0, flowerMaterial, matricesFlower.ToArray());
    }


    //private void SpawnGrassOnSurface(MeshFilter surface)
    //{
    //    Vector3[] vertices = surface.sharedMesh.vertices;
    //    Transform surfaceTransform = surface.transform;

    //    //float minDistance = 2f; //minimum distance between instances
    //    //List<Vector3> placedPositions = new List<Vector3>();

    //    int localCountA = 0, localCountB = 0;

    //    for (int i = 0; i < grassCountPerSurface; i++)
    //    {
    //        //if (placedPositions.Count >= grassCountPerSurface) break;

    //        int randomIndex = Random.Range(0, vertices.Length);
    //        Vector3 position = surfaceTransform.TransformPoint(vertices[randomIndex]);

    //        //randomize posotion
    //        position += new Vector3(
    //            Random.Range(-0.3f, 0.3f),
    //            0f,
    //            Random.Range(-0.3f, 0.3f)
    //        );

    //        ////check for overlap with already placed positions
    //        //bool isTooClose = false;
    //        //foreach (Vector3 placedPosition in placedPositions)
    //        //{
    //        //    if (Vector3.Distance(position, placedPosition) < minDistance)
    //        //    {
    //        //        isTooClose = true;
    //        //        break;
    //        //    }
    //        //}

    //        //if (isTooClose) continue;


    //        //50%chance spawning each grass mesh
    //        bool useMeshA = Random.value > 0.5f;

    //        if (useMeshA)
    //        {
    //            matricesA.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
    //            localCountA++;
    //        }
    //        else
    //        {
    //            matricesB.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
    //            localCountB++;
    //        }
    //    }

    //    countA += localCountA;
    //    countB += localCountB;

    //}


}
