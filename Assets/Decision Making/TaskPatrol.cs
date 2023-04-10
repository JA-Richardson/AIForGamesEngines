using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;

    }

    //public override NodeState Evaluate()
    //{
    //    if (transform.position == patrolPoints[targetPoint].position)
    //    {
    //        increaseTargetInt();
    //    }
    //    Vector3 directionToPoint = patrolPoints[targetPoint].position - transform.position;
    //    Quaternion lookAtPoint = Quaternion.LookRotation(directionToPoint);
    //    Vector3 rotationToPoint = Quaternion.Lerp(transform.rotation, lookAtPoint, Time.deltaTime * turnSpeed).eulerAngles;
    //    transform.rotation = Quaternion.Euler(0f, rotationToPoint.y, 0f);
    //    transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);


    //    state = NodeState.RUNNING;
    //    return state;
    //}

}