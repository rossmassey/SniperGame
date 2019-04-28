using UnityEngine;

/// <summary>
/// Determines if the enemy can see the player
/// </summary>
[RequireComponent(typeof(MeshCollider))]
public class EnemyVision : MonoBehaviour
{
    [HideInInspector]
    public bool canSeePlayer = false;
    [HideInInspector]
    public GameObject lastSightedPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            // TODO raycast towards player to check if player hidden behind cover
            canSeePlayer = true;
            lastSightedPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            canSeePlayer = false;
        }
    }

    private void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
        {
            Debug.LogError("EnemyVision collider not in ignore raycast layer");
        }
    }
}