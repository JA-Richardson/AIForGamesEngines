using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Script : MonoBehaviour
{

    public Quaternion direction;
    public float speed = 5;
    public float Turning = 2;

    List<GameObject> neighbours = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position += transform.up*speed*Time.deltaTime;
        
        direction = direction * Quaternion.Euler(new Vector3(Random.Range(-Turning, Turning), Random.Range(-Turning, Turning), Random.Range(-Turning, Turning)));
        

        

        this.transform.rotation = direction;
    }
}
