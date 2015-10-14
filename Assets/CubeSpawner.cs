using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    static public CubeSpawner C;

    public List<Cube> cubes = new List<Cube>();
    public float velocityMatchingAmt = 0.1f;
    public float velocityLerpAmt = 0.3f;
    public GameObject cubePrefab;
    public int numCubes = 50;
    public float spawnRadius = 100f;

    public float mouseAttractionAmt = 0.01f;
    public float mouseAvoidanceAmt = 20f;
    public float mouseAvoidanceDist = 20f;
    public Vector3 mousePos;

    // Use this for initialization
    void Start()
    {
        C = this;
        for (int i = 0; i < numCubes; i++)
        {
            cubes.Add(Instantiate(cubePrefab).GetComponent<Cube>());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        Vector3 mousePos2d =
        new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, this.transform.position.y);
        mousePos = GetComponent<Camera>().ScreenToWorldPoint(mousePos2d);
    }
}