using UnityEngine;

// TODO comment
public class EnemyBehavior : MonoBehaviour
{
    /*
     *
     *      State Machine:
     *
     *              IDLE -------(player enter vision)------------------> ALERT
     *
     *              ALERT ------(player not exit vision in t seconds)--> ENGAGING
     *              
     *              ALERT ------(t returns to 0)-----------------------> IDLE
     *
     *              ENGAGING ---(player exit vision)-------------------> SEARCHING
     *              
     *              SEARCHING --(player enter vision)------------------> ENGAGING
     *
     *              SEARCHING --(player not seen in x seconds)---------> IDLE
     *              
     *              t: engagingDelay
     *              x: returnToIdleDelay
     *
     *              IDLE:       movement along waypoints
     *              ALERT:      spotted target
     *              ENGAGING:   movement to target
     *              SEARCHING:  searching targets last seen position
     *
     *              This state machine is driven by EnemyVision, and manages
     *              EnemyPathing and EnemyAlertBar.
     *
     *
     */

    public float engagingDelay = 2.0f;
    public float returnToIdleDelay = 20.0f;

    public Color AlertColor;
    public Color EngagingColor;
    public Color SearchingColor;


    private enum State
    {
        IDLE, ALERT, ENGAGING, SEARCHING
    }

    private EnemyAlertBar alertBar;

    private EnemyPathing pathing;

    private EnemyVision vision;
    private bool canSeePlayer;
    private float timeSinceSpottedPlayer;

    private State currentState;

    private float alertTime;
    private float searchingTime;

    private void Start()
    {
        alertBar = GetComponentInChildren<EnemyAlertBar>();
        alertBar.DisableAlertBar();
        pathing = GetComponent<EnemyPathing>();
        vision = GetComponentInChildren<EnemyVision>();
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
                ManageIdle();
                break;

            case State.ALERT:
                ManageAlert();
                break;

            case State.ENGAGING:
                ManageEngaging();
                break;

            case State.SEARCHING:
                ManageSearching();
                break;
        }
    }

    private void ManageIdle()
    {
        if (canSeePlayer)
        {
            alertBar.SetForegroundColor(AlertColor);
            alertBar.EnableAlertBar();
            alertTime = 0;
            currentState = State.ALERT;
            return;
        }
    }

    private void ManageAlert()
    {
        if (canSeePlayer)
        {
            alertTime += Time.deltaTime;
            if (alertTime >= engagingDelay)
            {
                pathing.SetAlert(true);
                pathing.SetTarget(vision.lastSightedPlayer.transform.position);
                alertBar.SetForegroundColor(EngagingColor);
                currentState = State.ENGAGING;
                return;
            }
        }
        else
        {
            alertTime -= Time.deltaTime;
            if (alertTime <= 0)
            {
                alertBar.DisableAlertBar();
                currentState = State.IDLE;
                return;
            }
        }
        alertBar.SetForegroundFill(Mathf.Clamp(alertTime / engagingDelay, 0f, 1f));

    }

    private void ManageEngaging()
    {
        if (canSeePlayer)
        {
            pathing.SetTarget(vision.lastSightedPlayer.transform.position);
        }
        else
        {
            searchingTime = 0;
            alertBar.SetForegroundColor(SearchingColor);
            currentState = State.SEARCHING;
            return;
        }
    }

    private void ManageSearching()
    {
        if (canSeePlayer)
        {
            alertBar.SetForegroundFill(1f);
            alertBar.SetForegroundColor(EngagingColor);
            currentState = State.ENGAGING;
            return;
        }
        else
        {
            searchingTime += Time.deltaTime;
            if (searchingTime > returnToIdleDelay)
            {
                alertBar.DisableAlertBar();
                pathing.SetAlert(false);
                currentState = State.IDLE;
                return;
            }
        }
        alertBar.SetForegroundFill(Mathf.Clamp(1 - searchingTime / returnToIdleDelay, 0f, 1f));
    }

}