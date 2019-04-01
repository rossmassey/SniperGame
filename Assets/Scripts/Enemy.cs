using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 100.0f;
    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public float HealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public void DamageHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
