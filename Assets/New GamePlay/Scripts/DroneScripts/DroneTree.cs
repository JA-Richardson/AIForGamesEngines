using BehaviorTree;
using System.Collections.Generic;

public class DroneTree : Tree
{
    // Variables to be set in the inspector
    public UnityEngine.Transform[] patrolPoints; // Array of patrol points
    public UnityEngine.GameObject bulletPrefab; // Bullet prefab
    public UnityEngine.Transform chargerPrefab; // Charger prefab

    // Method that sets up the behavior tree
    protected override Node SetupTree()
    {
        // Create the root node of the tree, which is a selector node
        Node root = new Selector(new List<Node>
        {
            // First child of the root node is a sequence that checks the battery level and charges if needed
            new Sequence(new List<Node>
            {
                new CheckBattery(), // Check if the battery level is too low
                new ChargeBattery(transform, chargerPrefab) // Go to the charger and charge the battery
            }),
            // Second child of the root node is a sequence that checks for enemies, looks at them, and shoots at them
            new Sequence(new List<Node>
            {
                new CheckForEnemy(transform), // Check for enemies
                new LookAtTarget(transform), // Look at the enemy
                new Shoot(transform, bulletPrefab) // Shoot at the enemy
            }),
            // Third child of the root node is a patrol node
            new Patrol(transform, patrolPoints) // Patrol between the given patrol points
        });

        // Return the root node
        return root;
    }
}