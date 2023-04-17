using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public Transform target;
    public GridSystem gridSystem;

    public int numBoids = 20;
    public float spawnRange = 10;

    public float speed = 10f;
    public float rotationSpeed = 5;
    public float neighborDistance = 30.0f;
    public float separationDistance = 3.0f;
    public float separationWeight = 1.0f;
    public float alignmentWeight = 0.6f;
    public float cohesionWeight = 0.6f;
    public float avoidanceWeight = 4.0f;
    public float targetWeight = 1.0f;
    public float raycastDistance = 10.0f;
    public float fieldOfViewAngle = 120f;

    private List<Boid_Script> boids = new List<Boid_Script>();

    void Start()
    {
        for (int i = 0; i < numBoids; i++)
        {
            //GameObject boidInstance = Instantiate(boidPrefab, Random.insideUnitSphere * spawnRange, Random.rotation);
            GameObject boidInstance = Instantiate(boidPrefab,transform.position, Random.rotation);
            Boid_Script boidScript = boidInstance.GetComponent<Boid_Script>();
            boidScript.flockingManager = this;
            gridSystem.AddBoid(boidScript);
            boids.Add(boidScript);
        }
    }
}