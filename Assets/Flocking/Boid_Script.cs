using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Script : MonoBehaviour
{

    float speed = 20f;
    float rotationSpeed = 4.0f;
    float neighborDistance = 8.0f;
    float avoidanceDistance = 3.0f;
    float separationWeight = 1.0f;
    float alignmentWeight = 1.0f;
    float cohesionWeight = 0.9f;
    float avoidanceWeight = 5.0f;
    float raycastDistance = 5f;
    float spawnRange = 10;
    public LayerMask obstacleLayer;
    Vector3 separation;
    Vector3 alignment;
    Vector3 cohesion;
    Vector3 avoidance;

    GameObject[] boids;
    
    void Start()
    {
        this.transform.position = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange));

        this.transform.rotation = Random.rotation;


        boids = GameObject.FindGameObjectsWithTag("Boid");

        
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        var boidRenderer = this.GetComponent<Renderer>();

        boidRenderer.material.color = newColor;

    }

    void Update()
    {
       

        separation = Vector3.zero;
        alignment = Vector3.zero;
        cohesion = Vector3.zero;
        avoidance = Vector3.zero;
        int separationCount = 0;
        int alignmentCount = 0;
        int cohesionCount = 0;



        foreach (GameObject boid in boids)
        {
            if (boid != this.gameObject)
            {
                float distance = Vector3.Distance(boid.transform.position, this.transform.position);

                if (distance < avoidanceDistance)
                {
                    separation += this.transform.position - boid.transform.position;
                    separationCount++;
                }
                else if (distance < neighborDistance)
                {
                    alignment += boid.transform.up;
                    alignmentCount++;
                    cohesion += boid.transform.up;
                    cohesionCount++;
                }
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.up, out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal;
        }

        if (Physics.Raycast(this.transform.position, this.transform.up+new Vector3(10,0,0), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal;
        }
        if (Physics.Raycast(this.transform.position, this.transform.up + new Vector3(-10, 0, 0), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal;
        }
        if (Physics.Raycast(this.transform.position, this.transform.up + new Vector3(0, 0, 10), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal;
        }
        if (Physics.Raycast(this.transform.position, this.transform.up + new Vector3(0, 0, -10), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal;
        }
        if(avoidance!= Vector3.zero)
        {
            avoidance.Normalize();
            avoidance *= avoidanceWeight;
        }



        if (separationCount > 0)
        {
            separation /= separationCount;
            separation.Normalize();
            separation *= separationWeight;
        }

        if (alignmentCount > 0)
        {
            alignment /= alignmentCount;
            alignment.Normalize();
            alignment *= alignmentWeight;
        }

        if (cohesionCount > 0)
        {
            cohesion /= cohesionCount;
            cohesion -= this.transform.position;
            cohesion.Normalize();
            cohesion *= cohesionWeight;
        }




        Vector3 force = separation + alignment + cohesion + avoidance;
        this.transform.up = Vector3.Slerp(this.transform.up, force, Time.deltaTime * rotationSpeed);
        this.transform.position += this.transform.up * Time.deltaTime * speed;
    }
}
