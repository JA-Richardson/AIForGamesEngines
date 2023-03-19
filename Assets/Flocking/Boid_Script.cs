using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boid_Script : MonoBehaviour
{

    public float speed = 20f;
    public float rotationSpeed = 5;
    public float neighborDistance = 30.0f;
    public float separationDistance = 3.0f;
    public float separationWeight = 1.0f;
    public float alignmentWeight = 0.6f;
    public float cohesionWeight = 0.6f;
    public float avoidanceWeight = 4.0f;
    public float raycastDistance = 10.0f;
    public float spawnRange = 10;

    int separationCount = 0;
    int alignmentCount = 0;
    int cohesionCount = 0;

    public LayerMask obstacleLayer;
    public GameObject flockFollow;
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

        separationCount = 0;
        alignmentCount = 0;
        cohesionCount = 0;


        foreach (GameObject boid in boids)
        {
            if (boid != this.gameObject)
            {
                float distance = Vector3.Distance(boid.transform.position, this.transform.position);

                if (distance < separationDistance)
                {
                    separation += (this.transform.position - boid.transform.position) /Mathf.Pow(distance,2);
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
            avoidance += hit.normal / Mathf.Pow(hit.distance, 2); 
        }

        if (Physics.Raycast(this.transform.position, this.transform.up+new Vector3(10,0,0), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal / Mathf.Pow(hit.distance, 2); 
        }
        if (Physics.Raycast(this.transform.position, this.transform.up + new Vector3(-10, 0, 0), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal / Mathf.Pow(hit.distance, 2);
        }
        if (Physics.Raycast(this.transform.position, this.transform.up + new Vector3(0, 0, 10), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal / Mathf.Pow(hit.distance, 2);
        }
        if (Physics.Raycast(this.transform.position, this.transform.up + new Vector3(0, 0, -10), out hit, raycastDistance, obstacleLayer))
        {
            avoidance += hit.normal / Mathf.Pow(hit.distance, 2);
        }
        if(avoidance!= Vector3.zero)
        {
            //avoidance.Normalize();
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
