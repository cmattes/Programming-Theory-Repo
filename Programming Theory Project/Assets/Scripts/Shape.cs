using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject selectedShapeIndicator;
    public int vertices = 0;
    public float size = 0.5f;
    public int spawnRate = 1;
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
    private int timesSpawned = 0;

    private void Awake()
    {
        transform.localScale = new Vector3(size, size, size);
    }

    protected virtual void Update()
    {
        Move(new Rigidbody());

        selectedShapeIndicator.transform.position = transform.position;
    }

    private void OnMouseDown()
    {
        if(SimManager.Instance.SpawnerSelected)
        {
            SimManager.Instance.SendReferenceForSpawn(this);
            return;
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

    protected virtual void Move(Rigidbody rb)
    {
        var speed = 2;
        rb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }
}
