using UnityEngine;

// TODO comment
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
}