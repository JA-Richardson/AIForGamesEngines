using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Script : MonoBehaviour
{

    public Quaternion direction;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position += transform.up*speed*Time.deltaTime;
        this.transform.rotation = direction;
        
    }
}
