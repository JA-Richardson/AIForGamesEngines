using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Spawner : MonoBehaviour
{
    public GameObject boid;

    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 100; i++)
        {
            Instantiate(boid);
            
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
