using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public int vertices;
    public float size;
    public int spawnRate;
    public int TimesSpawned
    {
        get
        {
            return timesSpawned;
        }
        set
        {
            timesSpawned = value;
            CheckSpawnCount();
        }
    }
    private int timesSpawned;

    private void Start()
    {
        vertices = 0;
        size = 0.5f;
        spawnRate = 1;
        timesSpawned = 0;
    }

    private void OnMouseDown()
    {
        if(SimManager.Instance.SpawnerSelected)
        {
            SimManager.Instance.SendReferenceForSpawn(this);
        }

        SimManager.Instance.RequestSpawnShape(this);
    }

    private void CheckSpawnCount()
    {
        if(timesSpawned == spawnRate)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Move()
    {
        //do nothing
    }
}
