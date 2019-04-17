using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for an enemy that has a finite amount of health
/// </summary>
public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public float maxHealth = 100f;

    float currentHealth;
    Animator animator;
    NavMeshAgent navAgent;
    Rigidbody rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        // TODO allow enemy to walk and run based on "alert" state
        AnimateMovement();
    }

    /// <summary>
    /// Get percentage of enemy health
    /// </summary>
    /// <returns>Health percent</returns>
    public float HealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    /// <summary>
    /// Damage enemy health by certain amount 
    /// </summary>
    /// <param name="amount">How much damage to deal</param>
    public void DamageHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void AnimateMovement()
    {
        animator.SetFloat("speedPercent", navAgent.velocity.magnitude / navAgent.speed);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
