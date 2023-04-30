using BehaviorTree;
using UnityEngine;

public class Patrol : Node
{
    // Class variables
    public Transform[] _patrolPoints;  // An array of transforms for the points to patrol
    int targetPoint = 0;              // The current target patrol point
    float speed = 5f;                 // The speed at which to move towards the patrol points
    float turnSpeed = 10;             // The turn speed to use when rotating towards a patrol point
    private Transform _transform;     // The transform component of the GameObject this script is attached to

    // Constructor for the Patrol class
    public Patrol(Transform transform, Transform[] patrolPoints)
    {
        _transform = transform;
        _patrolPoints = patrolPoints;
    }

    // The Evaluate method is called by the behavior tree to update the state of this node
    public override NodeState Evaluate()
    {
        GameManager.Instance.DroneBattery -= 2.0f * Time.deltaTime;  // Reduce the drone battery level by 2 per second

        // If we've reached the current target patrol point, increment the target index to the next point
        if (_transform.position == _patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }

        // Calculate the direction and rotation towards the target patrol point
        Vector3 directionToPoint = _patrolPoints[targetPoint].position - _transform.position;
        Quaternion lookAtPoint = Quaternion.LookRotation(directionToPoint);
        Vector3 rotationToPoint = Quaternion.Lerp(_transform.rotation, lookAtPoint, Time.deltaTime * turnSpeed).eulerAngles;

        // Apply the rotation and movement to the drone
        _transform.rotation = Quaternion.Euler(0f, rotationToPoint.y, 0f);
        _transform.position = Vector3.MoveTowards(_transform.position, _patrolPoints[targetPoint].position, speed * Time.deltaTime);

        // Return RUNNING as the state of this node, as the drone is currently patrolling
        state = NodeState.RUNNING;
        return state;
    }

    // Method to increment the target patrol point index
    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= _patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
