using UnityEngine;
using System.Collections;
public class Cube : MonoBehaviour
{
    public Vector3 velocity; // The current velocity
    public Vector3 newVelocity; // The velocity for next frame
    public Vector3 newPosition; // The position for next frame

    void Awake()
    {
        Vector3 randPos = Random.insideUnitSphere * CubeSpawner.C.spawnRadius;
        randPos.y = 0;
        this.transform.parent = GameObject.Find("Cubes").transform;
        this.transform.position = randPos;
        velocity = Random.onUnitSphere;
        velocity *= 10f;
        // Give the Boid a random color, but make sure it's not too dark
        Color randColor = Color.black;
        while (randColor.r + randColor.g + randColor.b < 1.0f)
        {
            randColor = new Color(Random.value, Random.value, Random.value);
        }
        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            r.material.color = randColor;
        }
    }

    void Update()
    {
        // Initialize newVelocity and newPosition to the current values
        newVelocity = velocity;
        newPosition = this.transform.position;
        Vector3 randomVel = Random.insideUnitSphere;
        // Utilizes the fields set on the CubeSpawner.C Singleton
        newVelocity += randomVel * CubeSpawner.C.velocityMatchingAmt;
    }

    void LateUpdate()
    {
        // Adjust the current velocity based on newVelocity using a linear
        // interpolation
        velocity = (1 - CubeSpawner.C.velocityLerpAmt) * velocity +
        CubeSpawner.C.velocityLerpAmt * newVelocity;
        // Decide on the newPosition
        newPosition = this.transform.position + velocity * Time.deltaTime;
        // Keep everything in the XZ plane
        newPosition.y = 0;
        // Look from the old position at the newPosition to orient the model
        this.transform.LookAt(newPosition);
        // Actually move to the newPosition
        this.transform.position = newPosition;
    }

}