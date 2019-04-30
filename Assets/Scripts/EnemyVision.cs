using UnityEngine;

/// <summary>
/// Determines if the enemy can see the player
/// </summary>

public class EnemyVision : MonoBehaviour
{
    public float enemyViewRadius = 0.5f;
    public float enemyViewDistance = 25f;
    public float playerHeightOffset = 1f;

    public GameObject enemyHead;
    public GameObject player;
    public GameObject enemy;

    [HideInInspector]
    public bool canSeePlayer;

    private Vector3 playerDirectionFromEnemy;
    private Vector3 playerHeight;
    private float playerEnemyDotProduct;
    private float playerDistance;

    private void Start()
    {
        playerHeight = new Vector3(0, playerHeightOffset, 0);
    }

    private void Update()
    {
        CalculateVisionVectors();
        CheckForPlayer();
    }

    private void CalculateVisionVectors()
    {
        // get heading
        playerDirectionFromEnemy = player.transform.position - playerHeight - enemy.transform.position;

        // get dot product
        playerEnemyDotProduct = Vector3.Dot(-enemyHead.transform.forward, playerDirectionFromEnemy.normalized);

        // get distane
        playerDistance = Vector3.Distance(player.transform.position, enemy.transform.position);
    }

    private void CheckForPlayer()
    {
        if (playerEnemyDotProduct > enemyViewRadius && playerDistance < enemyViewDistance)
        {
            Ray ray = new Ray(enemyHead.transform.position, playerDirectionFromEnemy);
            Debug.DrawRay(enemyHead.transform.position, playerDirectionFromEnemy);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject firstHit = hit.transform.gameObject;
                if (firstHit.gameObject.tag.Equals("Player"))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
        }
        else
        {
            canSeePlayer = false;
        }
    }

    
}