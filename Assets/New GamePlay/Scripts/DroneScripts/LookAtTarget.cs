using BehaviorTree;
using UnityEngine;

public class LookAtTarget : Node
{
    private Transform _transform;
    public static Transform target; // Static variable to hold the target to look at
    public GameObject bulletPrefab;
    float turnSpeed = 10;

    public LookAtTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        target = CheckForEnemy.target; // Get the target to look at from the CheckForEnemy class
        if (target == null) // If there is no target, return failure
        {
            state = NodeState.FAILURE;
            return state;
        }

        // Calculate the direction to the target and use LookRotation to get a quaternion rotation
        Vector3 direction = target.position - _transform.position;
        direction.y = direction.y + 90; // Add 90 degrees to account for the model's default orientation
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Use Lerp to smoothly rotate towards the target
        Vector3 rotation = Quaternion.Lerp(_transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        // Set the new rotation of the object to face the target
        _transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Return running to indicate that the node is still being evaluated
        state = NodeState.RUNNING;
        return state;
    }
}