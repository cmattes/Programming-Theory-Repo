using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Shape
{
    private Rigidbody sphereRb;
    private int randomNum;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        vertices = 0;
        size = 1;
        spawnRate = 3;
        TimesSpawned = 0;
        sphereRb = GetComponent<Rigidbody>();
        StartCoroutine(GetRandomNumber());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    IEnumerator GetRandomNumber()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            randomNum = Random.Range(0, 4);
        }
    }

    protected override void Move()
    {
        switch(randomNum)
        {
            case 0:
                sphereRb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 1:
                sphereRb.AddForce(Vector3.back * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 2:
                sphereRb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Impulse);
                break;
            case 3:
            default:
                sphereRb.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.Impulse);
                break;
        }
    }

}
