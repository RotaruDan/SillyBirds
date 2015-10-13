using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    static public List<Cube> cubes;
    static public CubeSpawner C;
    public float velocityMatchingAmt = 0.1f;
    public float velocityLerpAmt = 0.1f;
    public GameObject cubePrefab;
    public int numCubes = 2;
    public float spawnRadius = 100f;

    public float mouseAttractionAmt = 0.02f;
    public float mouseAvoidanceAmt = 0.75f;
    public float mouseAvoidanceDist = 50f;
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
    /**
        public Vector3 GetAveragePosition( List<Boid> someBoids ) {
            Vector3 sum = Vector3.zero;
            foreach (Boid b in someBoids) {
                sum += b.transform.position;
            }
            Vector3 center = sum / someBoids.Count;
            return( center );
        }
    **/
    /**
        PROCEDURE rule2(boid bJ)
            Vector c = 0;
            FOR EACH BOID b
                IF b != bJ THEN
                    IF |b.position - bJ.position| < 100 THEN
                    c = c - (b.position - bJ.position)
                    END IF
                END IF
            END
            RETURN c
        END PROCEDURE
    **/

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

    /**
    public Vector3 GetAverageVelocity( List<Boid> someBoids ) {
        Vector3 sum = Vector3.zero;
        foreach (Boid b in someBoids) {
            sum += b.velocity;        }
        Vector3 avg = sum / someBoids.Count;
        return( avg );
    }
    **/
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