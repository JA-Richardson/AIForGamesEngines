using BehaviorTree; // import the namespace

using UnityEngine; // import the UnityEngine namespace

public class Shoot : Node // declare the Shoot class, which extends the Node class
{
    private Transform _transform; // private variable to store the transform of the shooter
    public static Transform target; // static variable to store the target, accessible from other classes
    public GameObject _bulletPrefab; // public variable to store the bullet prefab
    private float fireRate = 2; // variable to store the rate of fire
    private float fireCountdown = 0f; // variable to store the time until the next shot can be fired

    public Shoot(Transform transform, GameObject bulletPrefab) // constructor for the Shoot class that takes in the transform and bullet prefab as arguments
    {
        _transform = transform; // set the _transform variable to the given transform
        _bulletPrefab = bulletPrefab; // set the _bulletPrefab variable to the given bullet prefab
    }

    public override NodeState Evaluate() // override the Evaluate method of the Node class
    {
        GameManager.Instance.DroneBattery -= 2.0f * Time.deltaTime; // reduce the drone's battery life

        if (fireCountdown <= 0) // if the countdown to the next shot is zero or less
        {
            GameObject bulletGo = (GameObject)UnityEngine.Object.Instantiate(_bulletPrefab, _transform.position, _transform.rotation); // create a new instance of the bullet prefab
            BulletScript bullet = bulletGo.GetComponent<BulletScript>(); // get the BulletScript component of the bullet

            if (bullet != null) // if the BulletScript component exists
            {
                target = CheckForEnemy.target; // get the target from the CheckForEnemy class
                bullet.Seek(target); // set the bullet's target to the retrieved target
            }

            fireCountdown = 1f / fireRate; // reset the countdown to the next shot
        }

        fireCountdown -= Time.deltaTime; // reduce the countdown to the next shot

        state = NodeState.RUNNING; // set the state to RUNNING
        return state; // return the state
    }
}