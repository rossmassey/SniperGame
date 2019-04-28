using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for an enemy that has a finite amount of health
/// </summary>
public class EnemyCore : MonoBehaviour
{
    public string enemyName = "Enemy";
    public float maxHealth = 100f;

    private float currentHealth;
    private Rigidbody rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
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



    private void Die()
    {
        Destroy(gameObject);
    }
}