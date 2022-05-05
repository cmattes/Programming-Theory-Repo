using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    public static SimManager Instance;

    public GameObject SpherePrefab;
    public GameObject SphubePrefab;
    public GameObject CubePrefab;
    public bool SpawnerSelected { get; private set; }
    private Shape spawnerShape;
    private Shape referenceShape;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        SpawnerSelected = false;

        CreateOriginalSphere();
        CreateOriginalSphere();
    }

    private void CreateOriginalSphere()
    {
        float originalSize = 1;
        int originalSpawnRate = 3;

        GameObject sphere = Instantiate(SpherePrefab, Vector3.zero + Vector3.up, new Quaternion());

        sphere.GetComponent<Sphere>().Initialize(originalSize, originalSpawnRate);
    }

    public void RequestSpawnShape(Shape shape)
    {
        shape.selectedShapeIndicator.GetComponent<Renderer>().material.color = Color.red;
        shape.selectedShapeIndicator.SetActive(true);
        
        spawnerShape = shape;
        SpawnerSelected = true;
    }

    public void SendReferenceForSpawn(Shape refShape)
    {
        if(refShape.selectedShapeIndicator.activeSelf)
        {
            spawnerShape.selectedShapeIndicator.SetActive(false);
            SpawnerSelected = false;
            return;
        }
        
        refShape.selectedShapeIndicator.GetComponent<Renderer>().material.color = Color.blue;
        refShape.selectedShapeIndicator.SetActive(true);

        referenceShape = refShape;
        StartCoroutine(PrepForSpawn());
    }

    IEnumerator PrepForSpawn()
    {
        yield return new WaitForSeconds(3);

        SpawnShape();

        spawnerShape.selectedShapeIndicator.SetActive(false);
        referenceShape.selectedShapeIndicator.SetActive(false);

        spawnerShape.TimesSpawned++;
        SpawnerSelected = false;
    }

    private void SpawnShape()
    {
        int spawnVertices = DetermineSpawnVertices();

        float spawnSize = DetermineSpawnSize();

        if (spawnVertices >= 8)
        {
            SpawnCube(spawnSize);
            return;
        }

        if(spawnVertices <= 0)
        {
            SpawnSphere(spawnSize);
            return;
        }

        SpawnSphube(spawnVertices, spawnSize);
    }

    private int DetermineSpawnVertices()
    {
        int spawnVertices = referenceShape.vertices + 2;
        
        if (spawnerShape.vertices >= referenceShape.vertices)
        {
            spawnVertices = spawnerShape.vertices + 1;
        }

        if (spawnerShape.size <= referenceShape.size)
        {
            spawnVertices -= 1;
        }

        if (spawnerShape.spawnRate - spawnerShape.TimesSpawned == 1)
        {
            spawnVertices += 1;
        }

        return spawnVertices;
    }

    private float DetermineSpawnSize()
    {
        float spawnSize = referenceShape.size + 1;

        if (spawnerShape.size <= referenceShape.size)
        {
            spawnSize = spawnerShape.size / 2;
        }

        if (spawnerShape.spawnRate - spawnerShape.TimesSpawned == 1)
        {
            spawnSize = spawnerShape.size + 1.5f;
        }

        return spawnSize;
    }

    private void SpawnCube(float spawnSize)
    {
        int spawnSpawnRate = 2;

        if (spawnSize > 3)
        {
            spawnSpawnRate = 1;
        }

        GameObject cube = Instantiate(CubePrefab, spawnerShape.transform.position + Vector3.up, referenceShape.transform.rotation);

        cube.GetComponent<Cube>().Initialize(spawnSize, spawnSpawnRate);
    }

    private void SpawnSphere(float spawnSize)
    {
        int spawnSpawnRate = 3;

        if (spawnSize < 0.5)
        {
            spawnSpawnRate = 5;
        }
        else if (spawnSize < 1)
        {
            spawnSpawnRate = 4;
        }

        GameObject sphere = Instantiate(SpherePrefab, spawnerShape.transform.position + Vector3.up, referenceShape.transform.rotation);

        sphere.GetComponent<Sphere>().Initialize(spawnSize, spawnSpawnRate);
    }

    private void SpawnSphube(int spawnVertices, float spawnSize)
    {
        int spawnSpawnRate = 3;

        if (spawnVertices >= 6 || spawnSize > 3)
        {
            spawnSpawnRate = 2;
        }

        GameObject sphube = Instantiate(SphubePrefab, spawnerShape.transform.position + Vector3.up, referenceShape.transform.rotation);

        sphube.GetComponent<Sphube>().Initialize(spawnVertices, spawnSize, spawnSpawnRate);
    }
}
