using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Cube : Shape
{
    private Rigidbody cubeRb;
    private float speed;
    private float upSpeed;
    private int randomNum;

    public void Initialize(float size, int spawnRate)
    {
        vertices = 8;
        this.size = size;
        this.spawnRate = spawnRate;

        TimesSpawned = 0;
        speed = 20;
        upSpeed = 5;
        cubeRb = GetComponent<Rigidbody>();
        StartCoroutine(GetRandomNumber());

        base.Initialize();
    }

    // POLYMORPHISM
    protected override void Update()
    {
        if (!initialized)
        {
            return;
        }

        Move(cubeRb);

        selectedShapeIndicator.transform.position = transform.position;
    }

    protected IEnumerator GetRandomNumber()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            randomNum = Random.Range(0, 3);
        }
    }

    // POLYMORPHISM
    protected override void Move(Rigidbody rb)
    {
        switch (randomNum)
        {
            case 0:
                rb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
                rb.AddForce(Vector3.up * upSpeed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 1:
                rb.AddForce(Vector3.back * speed * Time.deltaTime, ForceMode.Impulse);
                rb.AddForce(Vector3.up * upSpeed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 2:
            default:
                rb.AddForce(Vector3.up * upSpeed * Time.deltaTime, ForceMode.Impulse);
                break;
        }
    }
}
