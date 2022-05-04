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

    }

    IEnumerator PrepForSpawn()
    {
        yield return new WaitForSeconds(3);

        spawnerShape.selectedShapeIndicator.SetActive(false);
        referenceShape.selectedShapeIndicator.SetActive(false);

        SpawnShape();
        spawnerShape.TimesSpawned++;
        SpawnerSelected = false;
    }
}
