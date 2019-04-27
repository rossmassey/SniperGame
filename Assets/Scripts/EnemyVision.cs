using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [HideInInspector]
    public bool canSeePlayer = false;
    [HideInInspector]
    public GameObject LastSightedPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Spotted player");

            canSeePlayer = true;
            LastSightedPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Lost sight player");

            canSeePlayer = false;
        }
    }
}