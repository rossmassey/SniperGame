using UnityEngine;

/// <summary>
/// Determines if the enemy can see the player
/// </summary>

public class EnemyVision : MonoBehaviour
{
    public float enemyViewRadius = 0.5f;
    public float enemyViewDistance = 25f;

    public bool canSeePlayer;
    public GameObject player;

    private GameObject enemy;
    private Vector3 lineToPlayer;
    private float playerToEnemyRadius;
    private float playerDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = transform.parent.gameObject;
    }

    private void Update()
    {
        lineToPlayer = player.transform.position - enemy.transform.position;
        playerToEnemyRadius = Vector3.Dot(enemy.transform.forward, lineToPlayer.normalized);
        playerDistance = Vector3.Distance(player.transform.position, enemy.transform.position);


        if (playerToEnemyRadius > enemyViewRadius && playerDistance < enemyViewDistance)
        {
            canSeePlayer = true;
        }
        else
        {
            canSeePlayer = false;
        }
    }
}