using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    /*
     *
     *      State Machine:
     *
     *              IDLE ------(spot player)--------------------------> ALERT
     *
     *              ALERT -----(player not exit vision in t seconds)--> ENGAGING
     *
     *              ENGAGING --(player exit vision)-------------------> SEARCHING
     *
     *              SEARCHING -----(player not seen in x seconds)---------> IDLE
     *
     *
     *              EnemyPathing:
     *              IDLE: movement to waypoints
     *              ENGAGING: movement to target
     *              ALERT: movement to targets last seen position
     *
     *              This state machine is driven by EnemyVision, and manages
     *              EnemyPathing.
     *
     *
     */

    public float engagingDelay = 2.0f;
    public float returnToIdleDelay = 20.0f;

    private enum State
    {
        IDLE, ALERT, ENGAGING, SEARCHING
    }

    private EnemyPathing pathing;

    private EnemyVision vision;
    private float timeSinceSpottedPlayer;
    private bool canSeePlayer;

    private State currentState;

    private float alertTime;
    private float searchingTime;

    private void Start()
    {
        vision = GetComponentInChildren<EnemyVision>();
        pathing = GetComponent<EnemyPathing>();
    }

    private void Update()
    {
        canSeePlayer = vision.canSeePlayer;
        ManageState();
    }

    private void ManageState()
    {
        switch (currentState)
        {
            case State.IDLE:
                Debug.Log("IDLE");
                ManageIdle();
                break;

            case State.ALERT:
                Debug.Log("ALERT");
                ManageAlert();
                break;

            case State.ENGAGING:
                Debug.Log("ENGAGING");
                ManageEngaging();
                break;

            case State.SEARCHING:
                Debug.Log("SEARCHING");
                ManageSearching();
                break;
        }
    }

    private void ManageSearching()
    {
        if (canSeePlayer)
        {
            currentState = State.ENGAGING;
        }
        else
        {
            searchingTime += Time.deltaTime;
            if (searchingTime > returnToIdleDelay)
            {
                pathing.SetAlert(false);
                currentState = State.IDLE;
            }
        }
    }

    private void ManageEngaging()
    {
        if (canSeePlayer)
        {
            pathing.SetTarget(vision.LastSightedPlayer.transform.position);
        }
        else
        {
            searchingTime = 0;
            currentState = State.SEARCHING;
        }
    }

    private void ManageAlert()
    {
        if (canSeePlayer)
        {
            alertTime += Time.deltaTime;
            if (alertTime > engagingDelay)
            {
                pathing.SetAlert(true);
                pathing.SetTarget(vision.LastSightedPlayer.transform.position);
                currentState = State.ENGAGING;
            }
        }
        else
        {
            currentState = State.IDLE;
        }
    }

    private void ManageIdle()
    {
        if (canSeePlayer)
        {
            alertTime = 0;
            currentState = State.ALERT;
        }
    }
}