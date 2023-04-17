using UnityEngine;

public class Boid_Spawner : MonoBehaviour
{
    public GameObject boid;


<<<<<<< Updated upstream
    public int spawnAmount = 150;

=======
    public int spawnAmount = 100;
    
>>>>>>> Stashed changes
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
