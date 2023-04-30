using BehaviorTree;
using UnityEngine;

public class ChargeBattery : Node
{
    public Transform _chargerLocation;     // The transform of the battery charger
    float speed = 5f;                      // Speed at which the drone moves towards the charger
    float turnSpeed = 10;                  // Speed at which the drone rotates towards the charger
    private Transform _transform;          // The transform of the drone

    public ChargeBattery(Transform transform, Transform chargerLocation)
    {
        _transform = transform;
        _chargerLocation = chargerLocation;
    }

    public override NodeState Evaluate()
    {
        // Calculate the direction vector from the drone to the charger location
        Vector3 directionToPoint = _chargerLocation.position - _transform.position;
        // Calculate the rotation needed to look towards the charger location
        Quaternion lookAtPoint = Quaternion.LookRotation(directionToPoint);
        // Smoothly interpolate the drone's rotation towards the charger location
        Vector3 rotationToPoint = Quaternion.Lerp(_transform.rotation, lookAtPoint, Time.deltaTime * turnSpeed).eulerAngles;
        _transform.rotation = Quaternion.Euler(0f, rotationToPoint.y, 0f);
        // Move the drone towards the charger location
        _transform.position = Vector3.MoveTowards(_transform.position, _chargerLocation.position, speed * Time.deltaTime);

        // If the drone is within 5 units of the charger location, increase the battery level
        if ((_chargerLocation.position - _transform.position).magnitude < 5)
        {
            GameManager.Instance.DroneBattery += 10.0f * Time.deltaTime;
        }

        state = NodeState.RUNNING;
        return state;
    }
}

