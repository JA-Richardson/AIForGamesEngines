
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boid_Script : MonoBehaviour
{
    public FlockingManager flockingManager;

    // Variables to keep track of the number of boids for each rule
    int separationCount = 0;
    int alignmentCount = 0;
    int cohesionCount = 0;

    public LayerMask obstacleLayer;

    // Force vectors for each rule
    Vector3 separation;
    Vector3 alignment;
    Vector3 cohesion;
    Vector3 avoidance;
    Vector3 target;

    

    void Start()
    {
        

        // Set the color of the boid
        float R = flockingManager.r;
        float G = flockingManager.g;
        float B = flockingManager.b;
        Color newColor = new Color(R, G, B, 1.0f);
        var boidRenderer = this.GetComponent<Renderer>();
        boidRenderer.material.color = newColor;
    }

    void Update()
    {
        // Destroy the boid if its FlockingManager is missing
        if (flockingManager == null)
        {
            Destroy(gameObject);
        }

        Vector3 oldPosition = transform.position;

        // Initialize forces and counts
        separation = Vector3.zero;
        alignment = Vector3.zero;
        cohesion = Vector3.zero;
        avoidance = Vector3.zero;
        target = Vector3.zero;
        separationCount = 0;
        alignmentCount = 0;
        cohesionCount = 0;

        // Get the neighboring boids
        List<Boid_Script> neighbors = flockingManager.gridSystem.GetNeighbors(this, flockingManager.neighborDistance);

        // Loop through the neighbors and calculate forces based on rules
        foreach (Boid_Script boid in neighbors)
        {
            if (boid != this.gameObject)
            {
                float distance = Vector3.Distance(boid.transform.position, this.transform.position);

                // Calculate forces only for boids in the field of view
                if (IsInFieldOfView(boid.transform.position))
                {
                    // Separation force
                    if (distance < flockingManager.separationDistance)
                    {
                        separation += (this.transform.position - boid.transform.position) / Mathf.Pow(distance, 2);
                        separationCount++;
                    }

                    // Alignment and cohesion forces
                    alignment += boid.transform.up;
                    alignmentCount++;
                    cohesion += boid.transform.up;
                    cohesionCount++;
                }
            }
        }

        // Calculate the avoidance force
        Tuple<Vector3, float> avoidanceResult = CalculateAvoidance();
        avoidance = avoidanceResult.Item1;
        float maxHitDistance = avoidanceResult.Item2;
        float proximityFactor = 1 - Mathf.Clamp01(maxHitDistance / flockingManager.raycastDistance);
        float adjustedRotationSpeed = flockingManager.rotationSpeed * (1 + proximityFactor * 3);  // Adjust boids turning speed when near an obstacle

        // Calculate the target force
        target = CalculateTargetForce();

        // Calculate the final forces for each rule
        if (separationCount > 0)
        {
            separation /= separationCount;
            separation.Normalize();
            separation *= flockingManager.separationWeight;
        }

        if (alignmentCount > 0)
        {
            alignment /= alignmentCount;
            alignment.Normalize();
            alignment *= flockingManager.alignmentWeight;
        }

        if (cohesionCount > 0)
        {
            cohesion /= cohesionCount;
            cohesion -= this.transform.position;
            cohesion.Normalize();
            cohesion *= flockingManager.cohesionWeight;
        }

        // Calculate the total force
        Vector3 force = separation + alignment + cohesion + avoidance + target;

        // Update the boid's direction and position based on the total force
        if (force != Vector3.zero)
        {
            this.transform.up = Vector3.Slerp(this.transform.up, force, Time.deltaTime * adjustedRotationSpeed);
        }
        this.transform.position += this.transform.up * Time.deltaTime * flockingManager.speed;

        // Update the grid system with the boid's new position
        flockingManager.gridSystem.RemoveBoid(this, oldPosition);
        flockingManager.gridSystem.AddBoid(this);
    }

    // Function to check if a target position is in the field of view
    private bool IsInFieldOfView(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angleToTarget = Vector3.Angle(transform.up, directionToTarget);

        return angleToTarget <= flockingManager.fieldOfViewAngle * 0.5f;
    }

    // Function to calculate the avoidance force
    private Tuple<Vector3, float> CalculateAvoidance()
    {
        Vector3 avoidanceForce = Vector3.zero;
        float[] raycastAngles = { 0, 30, -30, 60, -60 }; // Define multiple angles for raycasts
        float[] raycastDistances = { flockingManager.raycastDistance, flockingManager.raycastDistance * 0.5f }; // Define multiple distances for raycasts
        float maxHitDistance = 0f;

        foreach (float distance in raycastDistances)
        {
            foreach (float angle in raycastAngles)
            {
                Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.up;
                RaycastHit hit;

                if (Physics.Raycast(transform.position, rayDirection, out hit, distance, obstacleLayer))
                {
                    float avoidanceStrength = (1 - hit.distance / distance) * flockingManager.avoidanceWeight / Mathf.Pow(hit.distance, 2);
                    avoidanceForce += hit.normal * avoidanceStrength;

                    if (hit.distance > maxHitDistance)
                    {
                        maxHitDistance = hit.distance;
                    }
                }
            }
        }

        return new Tuple<Vector3, float>(avoidanceForce, maxHitDistance);
    }

    // Function to calculate the target force
    private Vector3 CalculateTargetForce()
    {
        if (flockingManager.target == null)
        {
            return Vector3.zero;
        }

        Vector3 directionToTarget = (flockingManager.target.position - transform.position).normalized;
        return directionToTarget * flockingManager.targetWeight;
    }



}
