using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Shape
{
    private Rigidbody cubeRb;
    private float speed;

    public void Initialize(float size, int spawnRate)
    {
        vertices = 8;
        this.size = size;
        this.spawnRate = spawnRate;

        TimesSpawned = 0;
        speed = 5;
        cubeRb = GetComponent<Rigidbody>();

        base.Initialize();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!initialized)
        {
            return;
        }

        Move(cubeRb);

        selectedShapeIndicator.transform.position = transform.position;
    }

    protected override void Move(Rigidbody rb)
    {
        switch (TimesSpawned)
        {
            case 0:
                rb.AddForce((Vector3.forward + Vector3.up) * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 1:
                rb.AddForce((Vector3.back + Vector3.up) * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 2:
            default:
                rb.AddForce(Vector3.up * Time.deltaTime, ForceMode.Impulse);
                break;
        }
    }
}
