using UnityEngine;

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
        Vector3 v1, v2, v3;
        v1 = rule1(this);
        v2 = rule2(this);
        v3 = rule3(this);

        newVelocity = velocity + v1 + v2 + v3;

        // Initialize newVelocity and newPosition to the current values
        //newVelocity = velocity;
        newPosition = this.transform.position;
        Vector3 randomVel = Random.insideUnitSphere;
        // Utilizes the fields set on the CubeSpawner.C Singleton
        newVelocity += randomVel * CubeSpawner.C.velocityMatchingAmt;

        Vector3 dist;
        dist = CubeSpawner.C.mousePos - this.transform.position;
        if (dist.magnitude > CubeSpawner.C.mouseAvoidanceDist)
        {
            newVelocity += dist * CubeSpawner.C.mouseAttractionAmt;
        }
        else
        {
            // If the mouse is too close, move away quickly!
            newVelocity -= dist.normalized * CubeSpawner.C.mouseAvoidanceAmt;
        }
    }

    void LateUpdate()
    {        
        // Adjust the current velocity based on newVelocity using a linear
        // interpolation
        velocity = (1 - CubeSpawner.C.velocityLerpAmt) * velocity +
        CubeSpawner.C.velocityLerpAmt * newVelocity;

        // Decide on the newPosition
        newPosition = this.transform.position + this.velocity * Time.deltaTime;

        // Keep everything in the XZ plane
        newPosition.y = 0;
        // Look from the old position at the newPosition to orient the model
        this.transform.LookAt(newPosition);
        // Actually move to the newPosition
        this.transform.position = newPosition;
        

    }

    /**
        PROCEDURE rule1(boid bJ)
            Vector pcJ
            FOR EACH BOID b
                IF b != bJ THEN
                pcJ = pcJ + b.position
                END IF
            END
            pcJ = pcJ / N-1
            RETURN (pcJ - bJ.position) / 100
        END PROCEDURE
    **/
    Vector3 rule1(Cube bJ)
    {
        Vector3 pcJ = Vector3.zero;
        foreach (Cube b in CubeSpawner.C.cubes)
        {
            if (b != bJ)
            {
                pcJ = pcJ + b.transform.position;
            }
        }
        pcJ = pcJ / (CubeSpawner.C.cubes.Count - 1);

        return (pcJ - bJ.transform.position) / 100;
    }

    /**
        PROCEDURE rule2(boid bJ)
            Vector c = 0;
            FOR EACH BOID b
                IF b != bJ THEN
                    IF b.position - bJ.position lt 100 THEN
                        c = c - (b.position - bJ.position)
                    END IF
                END IF
            END
            RETURN c
        END PROCEDURE
    **/
    Vector3 rule2(Cube bJ)
    {
        Vector3 c = Vector3.zero;
        foreach (Cube b in CubeSpawner.C.cubes)
        {
            if (b != bJ)
            {
                Vector3 len = b.transform.position - bJ.transform.position;
                if (len.magnitude < 2)
                {
                    c = c - (b.transform.position - bJ.transform.position);
                }
            }
        }
        return c;
    }

    /**
        PROCEDURE rule3(boid bJ)
            Vector pvJ
            FOR EACH BOID b
                IF b != bJ THEN
                    pvJ = pvJ + b.velocity
                END IF
            END
            pvJ = pvJ / N-1
            RETURN (pvJ - bJ.velocity) / 8
        END PROCEDURE
    **/
    Vector3 rule3(Cube bJ)
    {
        Vector3 pvJ = Vector3.zero;
        foreach (Cube b in CubeSpawner.C.cubes)
        {
            if (b != bJ)
            {
                pvJ = pvJ + b.velocity;
            }
        }
        pvJ = pvJ / (CubeSpawner.C.cubes.Count - 1);

        return (pvJ - bJ.velocity) / 8;
    }
    
}