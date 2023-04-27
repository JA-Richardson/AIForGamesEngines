using BehaviorTree;
using UnityEngine;

public class ChargeBattery : Node
{
    //GameObject[] patrolPointObjects;
    public Transform _chargerLocation;
    float speed = 5f;
    float turnSpeed = 10;
    private Transform _transform;

    public ChargeBattery(Transform transform, Transform chargerLocation)
    {
        _transform = transform;
        _chargerLocation = chargerLocation;
    }

    public override NodeState Evaluate()
    {
        Vector3 directionToPoint = _chargerLocation.position - _transform.position;
        Quaternion lookAtPoint = Quaternion.LookRotation(directionToPoint);
        Vector3 rotationToPoint = Quaternion.Lerp(_transform.rotation, lookAtPoint, Time.deltaTime * turnSpeed).eulerAngles;
        _transform.rotation = Quaternion.Euler(0f, rotationToPoint.y, 0f);
        _transform.position = Vector3.MoveTowards(_transform.position, _chargerLocation.position, speed * Time.deltaTime);

        if ((_chargerLocation.position - _transform.position).magnitude < 5)
        {
            GameManager.Instance.DroneBattery += 10.0f * Time.deltaTime;
        }


        state = NodeState.RUNNING;
        return state;


    }
}
