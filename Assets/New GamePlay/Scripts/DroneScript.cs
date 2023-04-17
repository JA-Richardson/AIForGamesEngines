using UnityEngine;

public class DroneScript : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    bool patrolling = false;

    public float range = 15.0f;
    public float fireRate = 1;
    private float fireCountdown = 0f;
    public string enemyTag = "Enemy";
    private Transform target;
    public GameObject bulletPrefab;
    public float turnSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        BulletScript bullet = bulletGo.GetComponent<BulletScript>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolling)
        {
            if (transform.position == patrolPoints[targetPoint].position)
            {
                increaseTargetInt();
            }
            Vector3 directionToPoint = patrolPoints[targetPoint].position - transform.position;
            Quaternion lookAtPoint = Quaternion.LookRotation(directionToPoint);
            Vector3 rotationToPoint = Quaternion.Lerp(transform.rotation, lookAtPoint, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotationToPoint.y, 0f);
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        }

        if (target == null)
        {
            patrolling = true;
            return;
        }
        if (target != null)
        {
            patrolling = false;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = direction.y + 90;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
