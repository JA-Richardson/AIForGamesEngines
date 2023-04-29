using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject Target;
    public GameObject destroyEffect;
    public GameObject damageEffect;
    public Material _machineLearningMAT;

    BaseScript baseScript;
    FlockingManager flockingManager;
    float health = 0f;

    public MeshRenderer rend;

    public float Red = 0.0f;
    public float Green = 0.0f;
    public float Blue = 0.0f;
    DNA dnaScript;
    //PopulationManager popman;
    //float HealthMultiplier = 1.0f;

    bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        //popman = gameObject.GetComponent<PopulationManager>();

        //HealthMultiplier = HealthMultiplier + (popman.Generation / 5);

        Target = GameObject.FindGameObjectWithTag("Base");
        baseScript = Target.GetComponent<BaseScript>();
        flockingManager = gameObject.GetComponent<FlockingManager>();
        rend = GetComponent<MeshRenderer>();
        health = (flockingManager.r + flockingManager.g + flockingManager.b) * 3;// * HealthMultiplier;
        dnaScript = gameObject.GetComponent<DNA>();
        

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = (Target.transform.position - transform.position).magnitude;
        if (distanceToTarget <= 5 && !IsDead)
        {
            baseScript.Damage(5);
            GameObject effectIns = (GameObject)Instantiate(damageEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);

            Destroy(flockingManager);
            gameObject.tag = "Dead";
            IsDead = true;
            rend.enabled = false;
            //Destroy(gameObject);
            gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
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
            Destroy(flockingManager);
            gameObject.tag = "Dead";
            IsDead = true;
            rend.enabled = false;
            gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            dnaScript.dead();
            //Destroy(gameObject);
            return;
        }
    }
}
