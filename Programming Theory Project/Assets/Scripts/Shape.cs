using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject selectedShapeIndicator;
    public int vertices = 0;
    public float size = 0.5f;
    public int spawnRate = 1;
    public int TimesSpawned // ENCAPSULATION
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
    protected bool initialized = false;
    private int timesSpawned = 0;

    protected virtual void Initialize()
    {
        transform.localScale = new Vector3(size, size, size);
        initialized = true;
    }

    protected virtual void Update()
    {
        if (!initialized)
        {
            return;
        }

        Move(new Rigidbody());

        selectedShapeIndicator.transform.position = transform.position;
    }

    // ABSTRACTION
    private void OnMouseDown()
    {
        if (!initialized)
        {
            return;
        }

        if (SimManager.Instance.SpawnerSelected)
        {
            SimManager.Instance.SendReferenceForSpawn(this);
            return;
        }

        SimManager.Instance.RequestSpawnShape(this);
    }

    // ABSTRACTION
    private void CheckSpawnCount()
    {
        if(timesSpawned == spawnRate)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Move(Rigidbody rb)
    {
        var speed = 2;
        rb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }
}
