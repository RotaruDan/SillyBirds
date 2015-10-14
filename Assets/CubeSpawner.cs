using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    static public List<Cube> cubes = new List<Cube>();
    static public CubeSpawner C;
    public float velocityMatchingAmt = 0.01f;
    public float velocityLerpAmt = 0.2f;
    public GameObject cubePrefab;
    public int numCubes = 25;
    public float spawnRadius = 100f;

    public float mouseAttractionAmt = 0.03f;
    public float mouseAvoidanceAmt = 0.55f;
    public float mouseAvoidanceDist = 40f;
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

    /**
        LOOP
            draw_boids()
            move_all_boids_to_new_positions()
        END LOOP
    **/
    // Update is called once per frame
    void Update()
    {
        move_all_boids_to_new_positions();
    }

    /**
        PROCEDURE move_all_boids_to_new_positions()
            Vector v1, v2, v3
            Boid b
            FOR EACH BOID b
                v1 = rule1(b)
                v2 = rule2(b)
                v3 = rule3(b)
                b.velocity = b.velocity + v1 + v2 + v3
                b.position = b.position + b.velocity
            END
        END PROCEDURE
    **/
    void move_all_boids_to_new_positions()
    {
        Vector3 v1, v2, v3;
        
        foreach(Cube cube in cubes)
        {
            v1 = rule1(cube);
            v2 = rule2(cube);
            v3 = rule3(cube);

            cube.velocity = cube.velocity + v1 + v2 + v3;
            cube.transform.position = cube.transform.position + cube.velocity;
        }

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
        foreach(Cube b in cubes)
        {
            if(b != bJ)
            {
                pcJ = pcJ + b.transform.position;
            }
        }
        pcJ = pcJ / (cubes.Count - 1);

        return (pcJ - bJ.transform.position) / 100;
    }

    public Vector3 GetAveragePosition(List<Cube> someBoids)
    {
        Vector3 sum = Vector3.zero;
        foreach (Cube b in someBoids)
        {
            sum += b.transform.position;
        }
        Vector3 center = sum / someBoids.Count;
        return (center);
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
        foreach(Cube b in cubes)
        {
            if(b != bJ)
            {
                Vector3 len = b.transform.position - bJ.transform.position;
                if (len.magnitude < 100)
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
        foreach (Cube b in cubes)
        {
            if(b != bJ)
            {
                pvJ = pvJ + b.velocity;
            }
        }
        pvJ = pvJ / (cubes.Count - 1);

        return (pvJ - bJ.velocity) / 80;
    }

    public Vector3 GetAverageVelocity(List<Cube> someBoids)
    {
        Vector3 sum = Vector3.zero;
        foreach (Cube b in someBoids)
        {
            sum += b.velocity;
        }
        Vector3 avg = sum / someBoids.Count;
        return (avg);
    }

    void LateUpdate()
    {
        Vector3 mousePos2d =
        new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, this.transform.position.y);
        mousePos = GetComponent<Camera>().ScreenToWorldPoint(mousePos2d);
    }
}