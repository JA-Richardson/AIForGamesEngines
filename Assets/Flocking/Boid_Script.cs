using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Script : MonoBehaviour
{

    float speed = 20f;
    float rotationSpeed = 4.0f;
    float neighborDistance = 25.0f;
    float avoidanceDistance = 5.0f;
    float separationWeight = 1.5f;
    float alignmentWeight = 1.0f;
    float cohesionWeight = 1.0f;
    float avoidanceWeight = 10.0f;
    float raycastDistance = 20f;
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

         // Get all the boids in the scene
        boids = GameObject.FindGameObjectsWithTag("Boid");


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


        // Calculate the separation, alignment, and cohesion vectors
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
        // Calculate the obstacle avoidance vector
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.up, out hit, raycastDistance, obstacleLayer))
        {
            avoidance = Vector3.Reflect(hit.normal, this.transform.up);
            avoidance.Normalize();
            avoidance *= avoidanceWeight;
        }

        // Apply the weights to the separation, alignment, and cohesion vectors
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



        // Apply the forces to the boid
        Vector3 force = separation + alignment + cohesion + avoidance;
        this.transform.up = Vector3.Slerp(this.transform.up, force, Time.deltaTime * rotationSpeed);
        this.transform.position += this.transform.up * Time.deltaTime * speed;
    }
}
