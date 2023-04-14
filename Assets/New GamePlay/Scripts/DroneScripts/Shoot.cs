using BehaviorTree;
using UnityEngine;

public class Shoot : Node
{
    private Transform _transform;
    public static Transform target;
    public GameObject _bulletPrefab;
    private float fireRate = 2;
    private float fireCountdown = 0f;

    public Shoot(Transform transform, GameObject bulletPrefab)
    {
        _transform = transform;
        _bulletPrefab = bulletPrefab;
    }

    public override NodeState Evaluate()
    {

        if (fireCountdown <= 0)
        {
            GameObject bulletGo = (GameObject)UnityEngine.Object.Instantiate(_bulletPrefab, _transform.position, _transform.rotation);
            BulletScript bullet = bulletGo.GetComponent<BulletScript>();

            if (bullet != null)
            {
                target = CheckForEnemy.target;
                bullet.Seek(target);
            }
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

        state = NodeState.RUNNING;
        return state;
    }
}