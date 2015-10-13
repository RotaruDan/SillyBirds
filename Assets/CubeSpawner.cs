using UnityEngine;
using System.Collections;
public class CubeSpawner : MonoBehaviour
{
    static public CubeSpawner C;
    public float velocityMatchingAmt = 0.1f;
    public float velocityLerpAmt = 0.1f;
    public GameObject cubePrefab;
    public int numCubes = 2;
    public float spawnRadius = 100f;
    // Use this for initialization
    void Start()
    {
        C = this;
        for (int i = 0; i < numCubes; i++)
        {
            Instantiate(cubePrefab);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}