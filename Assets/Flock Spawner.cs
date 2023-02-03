using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawner : MonoBehaviour
{

    public GameObject Boid;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {

            Transform transform = Boid.transform;

            Instantiate(Boid, transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
