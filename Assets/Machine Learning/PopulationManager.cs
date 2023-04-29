using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PopulationManager : MonoBehaviour
{

    public GameObject personPrefab;
    public int PopulationSize = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int TrialTime = 40; //round timer
    int Generation = 1;

    int count = 0;

    float spawndelay = 2;
    float spawncount = 0;

    public Transform[] spawners;

    GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation " + Generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time " + (int)elapsed, guiStyle);

    }


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("spawn");
        //while (count<PopulationSize )
        //{
        //    Vector3 pos = spawners[Random.Range(0, 4)].position; //changed to use 4 spawn points
        //    GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
        //    go.GetComponent<DNA>().R = Random.Range(0.0f, 1.0f);
        //    go.GetComponent<DNA>().G = Random.Range(0.0f, 1.0f);
        //    go.GetComponent<DNA>().B = Random.Range(0.0f, 1.0f);
        //    population.Add(go);
        //    count++;
        //    while (spawncount < spawndelay)
        //    {
        //        spawncount += Time.deltaTime;
        //    }
        //    spawncount = 0;
        //}



        //for (int i = 0; i < PopulationSize; i++)
        //{
            
        //    Vector3 pos = spawners[Random.Range(0, 4)].position; //changed to use 4 spawn points
        //    GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
        //    go.GetComponent<DNA>().R = Random.Range(0.0f, 1.0f);
        //    go.GetComponent<DNA>().G = Random.Range(0.0f, 1.0f);
        //    go.GetComponent<DNA>().B = Random.Range(0.0f, 1.0f);
        //    population.Add(go);

        //}
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < PopulationSize; i++)
        {

            Vector3 pos = spawners[Random.Range(0, 4)].position; //changed to use 4 spawn points
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().R = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().G = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().B = Random.Range(0.0f, 1.0f);
            population.Add(go);
            yield return new WaitForSeconds(2.0f);
        }
        yield return null;
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        offspring.GetComponent<DNA>().R = Random.Range(0, 10) < 5 ? dna1.R : dna2.R;
        offspring.GetComponent<DNA>().G = Random.Range(0, 10) < 5 ? dna1.G : dna2.G;
        offspring.GetComponent<DNA>().B = Random.Range(0, 10) < 5 ? dna1.B : dna2.B;

        return offspring;
    }
    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().TimeAlive).ToList();

        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        Generation++;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > TrialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
