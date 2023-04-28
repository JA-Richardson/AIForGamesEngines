using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject Target;
    public GameObject destroyEffect;
    public GameObject damageEffect;
    public Material _machineLearningMAT;

    BaseScript baseScript;
    FlockingManager flockingManager;
    public float health = 4f;

    public float Red = 0.0f;
    public float Green = 0.0f;
    public float Blue = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Base");
        baseScript = Target.GetComponent<BaseScript>();
        flockingManager = gameObject.GetComponent<FlockingManager>();



        //_machineLearningMAT.SetColor("MachineLearningMAT", new Color(Red,Green,Blue));
        //GetComponent<Renderer>().material.SetColor("MachineLearningMAT", new Color(Red,Green,Blue));

    }

    // Update is called once per frame
    void Update()
    {
        
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        var enemyRenderer = this.GetComponent<Renderer>();

        enemyRenderer.material.color = newColor;


        //Red = Random.Range(0.0f, 1.0f);
        //Green = Random.Range(0.0f, 1.0f);
        //Blue = Random.Range(0.0f, 1.0f);
        //var col = new Color(Red, Green, Blue, 0.5f);
        //GetComponent<Renderer>().material.color = col;



        float distanceToTarget = (Target.transform.position - transform.position).magnitude;
        if (distanceToTarget <= 5)
        {
            baseScript.Damage(5);
            GameObject effectIns = (GameObject)Instantiate(damageEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            Destroy(gameObject);
            return;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.AddCurrency(1);
            GameObject effectIns = (GameObject)Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(effectIns, 1f);
            Destroy(gameObject);
            return;
        }
    }
}
