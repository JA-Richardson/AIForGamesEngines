using BehaviorTree;
using UnityEngine;

public class CheckForEnemy : Node
{
    public string enemyTag = "Enemy";
    public static Transform target;
    float range = 20.0f;
    private Transform _transform;

    public CheckForEnemy(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(_transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;

            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            target = null;
            state = NodeState.FAILURE;
            return state;
        }
    }
    public Transform SetTarget()
    {
        return target;
    }
}
