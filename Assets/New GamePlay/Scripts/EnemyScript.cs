using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject Target;
    public GameObject destroyEffect;
    public GameObject damageEffect;
    BaseScript baseScript;
    public float health = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Base");
        baseScript = Target.GetComponent<BaseScript>();
    }

    // Update is called once per frame
    void Update()
    {
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
