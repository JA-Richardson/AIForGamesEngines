using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Script : MonoBehaviour
{

    public Quaternion direction;
    public Vector3 position;
    public float speed = 1;
    public float Turning = 5;
    public float spawnRange = 25;

    public List<GameObject> neighbours = new List<GameObject>();
    private Quaternion qTo;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.rotation;
        this.transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));

    }
    
    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

           
            //if (timer > 2)
            //{ // timer resets at 2, allowing .5 s to do the rotating
            //    qTo = Quaternion.Euler(new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f));
            //    timer = 0.0f;
            //}
            
           



        //this.transform.position += transform.up*speed*Time.deltaTime;

        direction = direction * Quaternion.Euler(new Vector3(Random.Range(-Turning, Turning), Random.Range(-Turning, Turning), Random.Range(-Turning, Turning)));

        transform.rotation = Quaternion.Slerp(transform.rotation, direction, Time.deltaTime * Turning);

        transform.Translate(Vector3.up * speed * Time.deltaTime);
        //this.transform.rotation = direction;
    }
    void allignment()
    {

    }
    void cohesion()
    {


    }
    void separation()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boid")
        {
            neighbours.Add(other.gameObject);
                    Debug.Log("trigger!");
        }
        
        
    }
    

    private void OnTriggerExit(Collider other)
    {
        neighbours.Remove(other.gameObject);
    }
}
