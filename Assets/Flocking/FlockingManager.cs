// FlockingManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The FlockingManager class is responsible for managing the flocking behavior
// of a group of boids.
public class FlockingManager : MonoBehaviour
{
    // Boid prefab and target for the boids to follow
    public GameObject boidPrefab;
    public Transform target;

    // Reference to the grid system for neighbor searching
    public GridSystem gridSystem;

    // Flocking settings
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

    // Color settings for boids
    public float r, g, b;

    // List of boids in the flock
    private List<Boid_Script> boids = new List<Boid_Script>();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the grid system
        gridSystem = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridSystem>();

        // Instantiate boids and add them to the grid system
        for (int i = 0; i < numBoids; i++)
        {
            GameObject boidInstance = Instantiate(boidPrefab, (Random.insideUnitSphere * spawnRange) + transform.position, Random.rotation);

            Boid_Script boidScript = boidInstance.GetComponent<Boid_Script>();
            boidScript.flockingManager = this;
            gridSystem.AddBoid(boidScript);
            boids.Add(boidScript);
        }

        // Set the color of the boids using the DNA script
        DNA dnaScript = gameObject.GetComponent<DNA>();
        r = dnaScript.R;
        g = dnaScript.G;
        b = dnaScript.B;
    }

    // Function to destroy all boids in the flock
    public void KillBoids()
    {
        for (int i = 0; i < numBoids; i++)
        {
            Destroy(boids[i]);
        }
        return;
    }
}