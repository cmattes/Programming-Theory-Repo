using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphube : Sphere
{
    [SerializeField] private GameObject sph;
    [SerializeField] private GameObject ube;
    private Rigidbody sphubeRb;

    public Sphube(int vertices, float size, int spawnRate)
    {
        this.vertices = vertices;
        this.size = size;
        this.spawnRate = spawnRate;
    }

    protected override void Start()
    {
        switch (vertices)
        {
            case 1:
                ube.transform.localPosition = new Vector3(0, 0.22f, 0);
                break;
            case 2:
                ube.transform.localPosition = new Vector3(0, 0.22f, -0.16f);
                break;
            case 3:
                ube.transform.localPosition = new Vector3(0, 0.22f, -0.36f);
                break;
            case 4:
                ube.transform.localPosition = new Vector3(-0.04f, 0.39f, -0.36f);
                break;
            case 5:
                ube.transform.localPosition = new Vector3(-0.04f, 0.39f, -0.58f);
                break;
            case 6:
                ube.transform.localPosition = new Vector3(-0.04f, 0.39f, -0.36f);
                sph.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            case 7:
            default:
                ube.transform.localPosition = new Vector3(-0.04f, 0.39f, -0.36f);
                sph.transform.localScale = new Vector3(1, 1, 1);
                break;
        }

        TimesSpawned = 0;
        speed = 5;
        sphubeRb = GetComponent<Rigidbody>();
        StartCoroutine(GetRandomNumber());
    }

    protected override void Update()
    {
        Move(sphubeRb);

        selectedShapeIndicator.transform.position = transform.position;
    }
}
