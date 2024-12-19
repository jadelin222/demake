using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

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
    public int grassCountPerSurface = 200; //instances

    [Header("Flower Settings")]
    public Mesh flowerMesh;
    public Mesh cutFlowerMesh;
    public Material flowerMaterial;
    public int flowerCountPerSurface = 50;

    [Header("Spwan Surfaces")]
    public List<SurfaceSettings> spawnSurfaces; //list of surfaces to spawn grass/grass+flowers on

    [Header("Particle Effect")]
    public GameObject cutEffectPrefab; // Prefab of the particle effect to play
    private List<ParticleSystem> activeParticles = new List<ParticleSystem>();
    private Queue<GameObject> particlePool = new Queue<GameObject>(); //pooling


    private List<Matrix4x4> matricesA = new List<Matrix4x4>(); //grass
    private List<Matrix4x4> matricesB = new List<Matrix4x4>(); //grass
    private List<Matrix4x4> matricesFlower = new List<Matrix4x4>();
    private List<Matrix4x4> matricesFlowersCut = new List<Matrix4x4>();

    private float minDistance = 0.4f;
    private List<Vector3> placedPositions = new List<Vector3>();
    //grass calculation
    private int initialGrassCount; 
    private int currentGrassCount;
    private int flowerCutCount;

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
        initialGrassCount = matricesA.Count + matricesB.Count;
        currentGrassCount = initialGrassCount;
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
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f); //random rotation
            //place the instance in the appropriate list
            if (meshB != null) //grass
            {
                bool useMeshA = Random.value > 0.5f;  //50%chance spawning each grass mesh
                if (useMeshA)                 //position, rotation, scale
                    matricesA.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
                else
                    matricesB.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
            }
            else if (matricesB == null) // flowers
                matricesFlower.Add(Matrix4x4.TRS(position, randomRotation, Vector3.one));
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            float percentage = GetGrassCutPercentage();
            Debug.Log($"grass cut percentage: {percentage:F2}%");
        }

        //draw using gpu instancing
        //grass A
        if (matricesA.Count > 0)
            Graphics.DrawMeshInstanced(grassMeshA, 0, grassMaterial, matricesA.ToArray());
        //grass B
        if (matricesB.Count > 0)
            Graphics.DrawMeshInstanced(grassMeshB, 0, grassMaterial, matricesB.ToArray());

        //flowers
        if (matricesFlower.Count > 0)
            Graphics.DrawMeshInstanced(flowerMesh, 0, flowerMaterial, matricesFlower.ToArray());
        //cut flowers
        if (matricesFlowersCut.Count > 0)
            Graphics.DrawMeshInstanced(cutFlowerMesh, 0, flowerMaterial, matricesFlowersCut.ToArray());
    }

    public void CutGrass(Vector3 cutPosition, float radius)
    {
        //grass A
        for (int i = 0; i < matricesA.Count; i++)
        {
            Vector3 grassPosition = matricesA[i].GetColumn(3); //extract position from matrix

            if (Vector3.Distance(grassPosition, cutPosition) <= radius)
            {
                //make grass small
                matricesA[i] = Matrix4x4.TRS(grassPosition, Quaternion.identity, Vector3.one * 0.5f);
                currentGrassCount--;

                TriggerCutEffect(grassPosition);
            }
        }

        //grass B 
        for (int i = 0; i < matricesB.Count; i++)
        {
            Vector3 grassPosition = matricesB[i].GetColumn(3);

            if (Vector3.Distance(grassPosition, cutPosition) <= radius)
            {
                matricesB[i] = Matrix4x4.TRS(grassPosition, Quaternion.identity, Vector3.one * 0.5f);
                currentGrassCount--;

                TriggerCutEffect(grassPosition);
            }      
        }

        //flowers
        for (int i = matricesFlower.Count - 1; i >= 0; i--) 
        {
            Vector3 flowerPosition = matricesFlower[i].GetColumn(3);

            if (Vector3.Distance(flowerPosition, cutPosition) <= radius)
            {
                //replace flower with cut flower mesh
                matricesFlowersCut.Add(Matrix4x4.TRS(flowerPosition, Quaternion.identity, Vector3.one));
                matricesFlower.RemoveAt(i); //remove the original flower
                flowerCutCount++;
                UIManager.Instance.ShowBottomScreenUI("A cold dread crawls over you as you cut the white flower");
            }
            if (flowerCutCount > 4)
            {
                //GameMaster.Instance.TriggerEnding(GameMaster.Endings.CutTooManyFlowers);
                FadeManager.Instance.FadeIn(() => GameMaster.Instance.TriggerEnding(Endings.CutTooManyFlowers));
            }
        }
    }
    public float GetGrassCutPercentage()
    {
        int grassCut = initialGrassCount - currentGrassCount;

        return (grassCut / (float)initialGrassCount) * 100f;
    }
    private void TriggerCutEffect(Vector3 position)
    {
        GameObject effect;
        if (particlePool.Count > 0)
        {
            effect = particlePool.Dequeue();
            effect.transform.position = position;
            effect.SetActive(true);
        }
        else
        {
            effect = Instantiate(cutEffectPrefab, position, Quaternion.identity);
        }

        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            StartCoroutine(ReturnParticleToPool(effect, ps.main.duration));
        }
    }
    private IEnumerator ReturnParticleToPool(GameObject effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        effect.SetActive(false);
        particlePool.Enqueue(effect);
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
