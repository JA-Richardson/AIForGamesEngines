using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public Slider healthBar;

    private void Start()
    {
        healthBar.maxValue = maxHealth;
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

    public void Update()
    {

        healthBar.value = currentHealth;
    }

}