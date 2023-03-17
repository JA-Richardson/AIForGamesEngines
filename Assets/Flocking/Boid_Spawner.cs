using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Spawner : MonoBehaviour
{
    public GameObject boid;


    int spawnAmount = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(boid);
            
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {


    }
}
