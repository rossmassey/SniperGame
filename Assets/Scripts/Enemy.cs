using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for an enemy that has a finite amount of health
/// </summary>
public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public float maxHealth = 100f;

    private float currentHealth;
    private Animator animator;
    private NavMeshAgent navAgent;
    private Rigidbody rb;

    private void Start()
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
    /// Damage an enemy
    /// </summary>
    /// <param name="tag">What part of enemy was hit</param>
    /// <param name="weaponDamage">How much damage the weapon does</param>
    public void Damage(string tag, float weaponDamage)
    {
        if (tag.Equals("Head"))
        {
            // headshot instant kill
            DamageHealth(currentHealth);
        }
        else
        {
            DamageHealth(weaponDamage);
        }
    }

    private void DamageHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void AnimateMovement()
    {
        float speedPercent = navAgent.velocity.magnitude / navAgent.speed;
        animator.SetFloat("speedPercent", speedPercent, 0.1f, Time.deltaTime); // TODO create smooth variable
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}