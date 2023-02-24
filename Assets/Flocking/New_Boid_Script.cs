using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Boid_Script : MonoBehaviour
{

    public Vector3 Velocity;
    public float speed = 10;
    float turningRate = 3;
    float spawnRange = 25;
    float a = 0.9f;
    float c = 1.0f;
    float s = 1.0f;

    public List<GameObject> neighbours = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange));

        this.transform.rotation = Random.rotation;

        this.Velocity = transform.up;

    }

    // Update is called once per frame
    void Update()
    {

        if (neighbours.Count != 0)
        {
            Vector3 flockVelocity = new Vector3(0, 0, 0);

            flockVelocity += allignment()*a;
            flockVelocity += cohesion()*c;
            flockVelocity += Separation()*s;
            flockVelocity.Normalize();


            Velocity = Vector3.Lerp(Velocity, flockVelocity, turningRate * Time.deltaTime);

        }



        transform.position += Velocity * speed * Time.deltaTime;





    }

    Vector3 allignment()
    {
        Vector3 Average = new Vector3(0, 0, 0);


        foreach (var neigbour in neighbours)
        {
            Average += neigbour.transform.up;
        }

        Average.Normalize();
        return Average;
    }
    Vector3 cohesion()
    {
        Vector3 average = new Vector3(0, 0, 0);

        foreach (var neighbour in neighbours)
        {

            average += neighbour.transform.position;



        }
        average /= neighbours.Count;

        Vector3 vectorToAverage = average - this.transform.position;



        return vectorToAverage;

    }
    Vector3 Separation()
    {
        Vector3 average = new Vector3(0, 0, 0);

        foreach (var neighbour in neighbours)
        {

            average += (this.transform.position - neighbour.transform.position);



        }
        average /= neighbours.Count;

        return average;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boid")
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
