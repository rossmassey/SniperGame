using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Two state pathing, following waypoints or a target
/// </summary>
public class EnemyPathing : MonoBehaviour
{
    public GameObject[] waypoints;
    public float waypointDelay = 5f;
    public float waypointMinDistance = 2f;

    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    private float timeSinceArrival = 0;

    private bool isAlerted = false;
    private Vector3 targetLocation;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // default to moving along waypoints, (IDLE)
        agent.destination = waypoints[0].transform.position;
    }

    private void Update()
    {
        if (isAlerted)
        {
            MoveTowardsTarget();
        }
        else
        { 
            MoveAlongWaypoints();
        }
        
    }

    public void SetAlert(bool alerted)
    {
        isAlerted = alerted;
        if (alerted == false)
        {
            // return to last waypoint
            agent.destination = waypoints[currentWaypoint].transform.position;
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        targetLocation = newTarget;
    }

    private void MoveTowardsTarget()
    {
        agent.destination = targetLocation;
        // TODO stop when within radius of target location
        // TODO look (rotate) around when stopped
    }

    private void MoveAlongWaypoints()
    {
        // wait until agent arrives at destination
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < waypointMinDistance)
        {
            timeSinceArrival += Time.deltaTime;
        }

        if (timeSinceArrival >= waypointDelay)
        {
            SetWaypoint();
            timeSinceArrival = 0;
        }
    }

    private void SetWaypoint()
    {
        if (currentWaypoint == waypoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint += 1;
        }
        agent.destination = waypoints[currentWaypoint].transform.position;
    }
}