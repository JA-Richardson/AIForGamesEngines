using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Spawner : MonoBehaviour
{
    public GameObject boid;

    int i = 0;
    int spawnAmount = 1000;
    
    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(i<spawnAmount)
        {
            Instantiate(boid);
            i++;
        }
        

    }
}
