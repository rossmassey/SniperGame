using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public float maxHealth = 100f;
    public float cleanupDelay = 10f;

    float currentHealth;
    Rigidbody rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
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

    /* 
    public void AddForce(Vector3 origin, float forceAmount)
    {
        Vector3 forceDirection = transform.position - origin;
        forceDirection = Vector3.Normalize(forceDirection);
        rb.AddForce(forceDirection * forceAmount);
        // TODO use ray to do AddForceAtPosition instead
    }
    */

    void Die()
    {

        //yield return new WaitForSeconds(cleanupDelay);
        Destroy(gameObject);
    }

}
