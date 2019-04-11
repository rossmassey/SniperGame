using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathing : MonoBehaviour
{

    public GameObject[] waypoints;
    public float waypointDelay = 5f;
    public float waypointMinDistance = 2f;

    NavMeshAgent agent;

    int currentWaypoint = 0;
    float timeSinceArrival = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[0].transform.position;
    }

    private void Update()
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
            //Debug.Log("Moving to waypoint #" + currentWaypoint);
            MoveToNextWaypoint();
            timeSinceArrival = 0;
        }
    }

    void SetWaypoint()
    {
        if (currentWaypoint == waypoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint += 1;
        }  
    }

    void MoveToNextWaypoint()
    {
        agent.destination = waypoints[currentWaypoint].transform.position;
    }





}
