using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject EnemyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 5f);
    }

    void SpawnEnemy()
    {
        Instantiate(EnemyToSpawn, transform.position, transform.rotation);
        Debug.Log("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
