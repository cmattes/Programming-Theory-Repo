using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    public static SimManager Instance;

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
        if(string.Equals(spawnerShape.name, refShape.name))
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

        Shape cube = new Cube(spawnSize, spawnSpawnRate);

        Instantiate(cube, spawnerShape.transform.position + Vector3.up, referenceShape.transform.rotation);
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

        Shape sphere = new Sphere(spawnSize, spawnSpawnRate);

        Instantiate(sphere, spawnerShape.transform.position + Vector3.up, referenceShape.transform.rotation);
    }

    private void SpawnSphube(int spawnVertices, float spawnSize)
    {
        int spawnSpawnRate = 3;

        if (spawnVertices >= 6 || spawnSize > 3)
        {
            spawnSpawnRate = 2;
        }

        Shape sphube = new Sphube(spawnVertices, spawnSize, spawnSpawnRate);

        Instantiate(sphube, spawnerShape.transform.position + Vector3.up, referenceShape.transform.rotation);
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
}
