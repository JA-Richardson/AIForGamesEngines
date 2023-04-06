using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    GameObject Target;
    
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
        float distanceToTarget =  (Target.transform.position - transform.position).magnitude;
        if(distanceToTarget <= 7)
        {
            baseScript.Damage(5);
            Destroy(gameObject);
            return;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}
