using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Simple waypoint system for enemies to follow
/// </summary>
public class EnemyPathing : MonoBehaviour
{

    public GameObject[] waypoints;
    public float waypointDelay = 5f;
    public float waypointMinDistance = 2f;

    NavMeshAgent agent;

    int currentWaypoint = 0;
    float timeSinceArrival = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[0].transform.position;
    }

    void Update()
    {
        WaitAndMove();
    }

    private void WaitAndMove()
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
