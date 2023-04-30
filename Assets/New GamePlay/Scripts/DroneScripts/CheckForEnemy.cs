using BehaviorTree;
using UnityEngine;

public class CheckForEnemy : Node
{
    // The tag of the enemies to look for
    public string enemyTag = "Enemy";
    // The transform of the nearest enemy (static so it can be accessed from other scripts)
    public static Transform target;
    // The range within which to look for enemies
    float range = 20.0f;
    // The transform of the object that this script is attached to
    private Transform _transform;

    // Constructor that sets the _transform variable to the transform of the object that this script is attached to
    public CheckForEnemy(Transform transform)
    {
        _transform = transform;
    }

    // Method that checks for enemies within range and sets the target transform to the nearest one
    public override NodeState Evaluate()
    {
        // Find all game objects with the enemyTag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // Set the shortest distance to infinity and the nearest enemy to null
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Iterate through all enemies and find the nearest one
        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance to the current enemy
            float distanceToEnemy = Vector3.Distance(_transform.position, enemy.transform.position);
            // If the distance to the current enemy is shorter than the shortest distance, set the shortest distance to the distance to the current enemy and the nearest enemy to the current enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // If there is a nearest enemy within range, set the target transform to the transform of the nearest enemy and return SUCCESS
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;

            state = NodeState.SUCCESS;
            return state;
        }
        // If there is no nearest enemy within range, set the target transform to null and return FAILURE
        else
        {
            target = null;
            state = NodeState.FAILURE;
            return state;
        }
    }

    // Method to get the target transform
    public Transform SetTarget()
    {
        return target;
    }
}
