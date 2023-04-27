using BehaviorTree;
using UnityEngine;

public class Patrol : Node
{
    //GameObject[] patrolPointObjects;
    public Transform[] _patrolPoints;
    int targetPoint = 0;
    float speed = 5f;
    float turnSpeed = 10;
    private Transform _transform;

    public Patrol(Transform transform, Transform[] patrolPoints)
    {
        _transform = transform;
        _patrolPoints = patrolPoints;
    }

    public override NodeState Evaluate()
    {
        GameManager.Instance.DroneBattery -= 2.0f * Time.deltaTime;
        if (_transform.position == _patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }
        Vector3 directionToPoint = _patrolPoints[targetPoint].position - _transform.position;
        Quaternion lookAtPoint = Quaternion.LookRotation(directionToPoint);
        Vector3 rotationToPoint = Quaternion.Lerp(_transform.rotation, lookAtPoint, Time.deltaTime * turnSpeed).eulerAngles;
        _transform.rotation = Quaternion.Euler(0f, rotationToPoint.y, 0f);
        _transform.position = Vector3.MoveTowards(_transform.position, _patrolPoints[targetPoint].position, speed * Time.deltaTime);


        state = NodeState.RUNNING;
        return state;
    }

    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= _patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
