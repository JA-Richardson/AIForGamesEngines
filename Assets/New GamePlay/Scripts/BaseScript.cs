using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth = 100f;

//    public HealthBar

    //BulletScript bullet = bulletGo.GetComponent<BulletScript>();

    private void Start()
    {
        
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

}
