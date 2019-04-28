using UnityEngine;

/// <summary>
/// Determines if the enemy can see the player
/// </summary>

public class EnemyVision : MonoBehaviour
{
    public float enemyViewRadius = 0.5f;
    public float enemyViewDistance = 25f;
    public Vector3 playerHeightOffset = new Vector3(0, 2f, 0);

    public bool canSeePlayer;
    public CapsuleCollider playerCollider;

    private GameObject enemy;
    private Vector3 lineToPlayer;
    private float playerToEnemyRadius;
    private float playerDistance;

    private void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
        enemy = transform.parent.gameObject;
    }

    private void Update()
    {
        lineToPlayer = (playerCollider.transform.position - playerHeightOffset) - enemy.transform.position;
        playerToEnemyRadius = Vector3.Dot(enemy.transform.forward, lineToPlayer.normalized);
        playerDistance = Vector3.Distance(playerCollider.transform.position, enemy.transform.position);

        if (playerToEnemyRadius > enemyViewRadius && playerDistance < enemyViewDistance)
        {
            // draw ray from EnemyVision gameObject (located at head)
            Ray ray = new Ray(transform.position, lineToPlayer);
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