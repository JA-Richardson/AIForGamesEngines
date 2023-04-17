
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boid_Script : MonoBehaviour
{
    public FlockingManager flockingManager;



    int separationCount = 0;
    int alignmentCount = 0;
    int cohesionCount = 0;

    public LayerMask obstacleLayer;
    
    Vector3 separation;
    Vector3 alignment;
    Vector3 cohesion;
    Vector3 avoidance;
    Vector3 target;

    GameObject[] boids;

    void Start()
    {

        



        boids = GameObject.FindGameObjectsWithTag("Boid");


        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        var boidRenderer = this.GetComponent<Renderer>();

        boidRenderer.material.color = newColor;


    }

    void Update()
    {





            Vector3 oldPosition = transform.position;



            separation = Vector3.zero;
            alignment = Vector3.zero;
            cohesion = Vector3.zero;
            avoidance = Vector3.zero;
            target = Vector3.zero;

            separationCount = 0;
            alignmentCount = 0;
            cohesionCount = 0;


            List<Boid_Script> neighbors = flockingManager.gridSystem.GetNeighbors(this, flockingManager.neighborDistance);

            foreach (Boid_Script boid in neighbors)
            {
                if (boid != this.gameObject)
                {
                    float distance = Vector3.Distance(boid.transform.position, this.transform.position);

                    

                        if (IsInFieldOfView(boid.transform.position))
                        {
                            if (distance < flockingManager.separationDistance)
                            {

                                separation += (this.transform.position - boid.transform.position) / Mathf.Pow(distance, 2);
                                separationCount++;
                            }



                            alignment += boid.transform.up;
                            alignmentCount++;
                            cohesion += boid.transform.up;
                            cohesionCount++;
                        }

                    




                }
            }



            Tuple<Vector3, float> avoidanceResult = CalculateAvoidance();
            avoidance = avoidanceResult.Item1;
            float maxHitDistance = avoidanceResult.Item2;
            float proximityFactor = 1 - Mathf.Clamp01(maxHitDistance / flockingManager.raycastDistance);
            float adjustedRotationSpeed = flockingManager.rotationSpeed * (1 + proximityFactor * 3);  // adjust boids turning speed when near an obstical


            target = CalculateTargetForce();

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




            Vector3 force = separation + alignment + cohesion + avoidance + target;
            if (force != Vector3.zero)
            {
                this.transform.up = Vector3.Slerp(this.transform.up, force, Time.deltaTime * adjustedRotationSpeed);
            }
            this.transform.position += this.transform.up * Time.deltaTime * flockingManager.speed;

            flockingManager.gridSystem.RemoveBoid(this, oldPosition);
            flockingManager.gridSystem.AddBoid(this);
        

        

    }
    private bool IsInFieldOfView(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angleToTarget = Vector3.Angle(transform.up, directionToTarget);

        return angleToTarget <= flockingManager.fieldOfViewAngle * 0.5f;
    }
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
