using System.Collections;
using System.Collections.Generic;
using BehaviorTree;

public class DroneTree : Tree
{
    public UnityEngine.Transform[] patrolPoints;
    public UnityEngine.GameObject bulletPrefab;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckForEnemy(transform),
                new LookAtTarget(transform),
                new Shoot(transform, bulletPrefab)
            }),
            new Patrol(transform, patrolPoints)//, waypoints),
        });

        return root;
    }

}
