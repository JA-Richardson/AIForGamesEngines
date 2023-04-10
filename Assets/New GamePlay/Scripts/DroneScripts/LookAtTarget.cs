using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BehaviorTree;
using static UnityEngine.GraphicsBuffer;

public class LookAtTarget : Node
{
    private Transform _transform;
    public static Transform target;
    //private Transform _target;
    public GameObject bulletPrefab;
    float turnSpeed = 10;

    public LookAtTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        target = CheckForEnemy.target;
        if (target== null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Vector3 direction = target.position - _transform.position;
        direction.y = direction.y + 90;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        _transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        state = NodeState.RUNNING;
        return state;
    }
}