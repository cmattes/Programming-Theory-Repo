using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Shape
{
    private Rigidbody sphereRb;
    private int randomNum;
    protected float speed;

    public void Initialize(float size, int spawnRate)
    {
        vertices = 0;
        this.size = size;
        this.spawnRate = spawnRate;

        TimesSpawned = 0;
        speed = 5;
        sphereRb = GetComponent<Rigidbody>();
        StartCoroutine(GetRandomNumber());

        base.Initialize();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!initialized)
        {
            return;
        }

        Move(sphereRb);

        selectedShapeIndicator.transform.position = transform.position;
    }

    protected IEnumerator GetRandomNumber()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            randomNum = Random.Range(0, 4);
        }
    }

    protected override void Move(Rigidbody rb)
    {
        switch(randomNum)
        {
            case 0:
                rb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 1:
                rb.AddForce(Vector3.back * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 2:
                rb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 3:
            default:
                rb.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.Impulse);
                break;
        }
    }

}
