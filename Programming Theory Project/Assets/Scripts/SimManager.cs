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
        SpawnerSelected = true;
        spawnerShape = shape;
        // turn on red indicator
    }

    public void SendReferenceForSpawn(Shape refShape)
    {
        referenceShape = refShape;
        // turn on blue indicator
        StartCoroutine(PrepForSpawn());
    }

    private void SpawnShape()
    {

    }

    IEnumerator PrepForSpawn()
    {
        yield return new WaitForSeconds(3);
        SpawnerSelected = false;
        // turn off red and blue indicators
        SpawnShape();
        spawnerShape.TimesSpawned++;
    }
}
