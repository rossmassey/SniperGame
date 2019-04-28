using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Two state pathing, following waypoints or a target
/// </summary>
public class EnemyPathing : MonoBehaviour
{
    public float runningSpeed = 4f;
    public float walkingSpeed = 2f;

    public GameObject[] waypoints;
    public float waypointDelay = 5f;
    public float waypointMinDistance = 2f;

    private NavMeshAgent navAgent;
    private int currentWaypoint = 0;
    private float timeSinceArrival = 0f;

    private Animator animator;

    private bool isAlerted = false;
    private Vector3 targetLocation;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        navAgent.destination = waypoints[0].transform.position;
        navAgent.speed = walkingSpeed;
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

        AnimateMovement();
    }

    private void AnimateMovement()
    {
        float speedPercent = navAgent.velocity.magnitude / runningSpeed;
        animator.SetFloat("speedPercent", speedPercent, 0.1f, Time.deltaTime); // TODO create smooth variable
    }

    public void SetAlert(bool alerted)
    {
        if (alerted == false)
        {
            // return to last waypoint
            navAgent.destination = waypoints[currentWaypoint].transform.position;
            navAgent.speed = walkingSpeed;
            isAlerted = false;
        }
        else
        {
            isAlerted = true;
            navAgent.speed = runningSpeed;
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        targetLocation = newTarget;
    }

    private void MoveTowardsTarget()
    {
        navAgent.destination = targetLocation;
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
        navAgent.destination = waypoints[currentWaypoint].transform.position;
    }
}