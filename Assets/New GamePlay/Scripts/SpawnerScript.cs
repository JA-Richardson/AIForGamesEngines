using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject EnemyToSpawn;
    public bool Active;
    public GameObject spawnEffect;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnEnemy", 0f, 7f);
    }

    void SpawnEnemy()
    {
        if (Active)
        {
            Instantiate(EnemyToSpawn, transform.position, transform.rotation);
            GameObject effectIns = (GameObject)Instantiate(spawnEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
