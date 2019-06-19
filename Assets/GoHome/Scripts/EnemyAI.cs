using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public float moveSpeed;
    public float sprintSpeed;
    public float stoppingDistance = 1f;
    public float gravityDistance = 2f;
    

    public enum State
    {
        patrol,
        seek
    }
    public enum PatrolMethod
    {
        loop,
        endToEnd,
        wander
    }
    public State currentTask;
    public PatrolMethod patrolBehaviour;
    public Transform patrolRoute;
    public GameObject target;

    NavMeshAgent na;
    // Rigidbodies don't play nice with Navmesh agents. if you need to apply physics to an enemy, only enable it when it is specifically affected by something meant to move or rotate the enemy using physics, then disable once normal AI needs to resume.
    // E.g. if an enemy is knocked back by an explosion, have the explosion enable the rigidbody and disable the navmesh agent on some kind of stun timer
    // When the enemy recovers from being stunned, disable the rigidbody and reenable the navmesh agent.

    bool endForward = true;
    private Transform[] waypoints; //Array of different scripts
    private int currentIndex = 1;



    // Use this for initialization
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        na.speed = moveSpeed;
        //na.autoBraking = false;

        waypoints = patrolRoute.GetComponentsInChildren<Transform>(); // Obtains Transform script info from all child objects of 'patrolRoute', which are the waypoints. You can have multiple arrays and versions of this command to obtain multiple different scipts from a set of objects.

    }

    // Update is called once per frame
    void Update()
    {
        



        switch (currentTask)
        {
            case State.seek:
                Seek();
                break;
            case State.patrol:
                Patrol();
                break;
            default:
                break;
        }

        
        //AirSupply -= BreatheRate * Time.deltaTime;
        //print(Mathf.Round(AirSupply));

        //print(waypoints.Length);
    }

    
    void OnDrawGizmosSelected()
    {
        // If waypoints is not null AND waypoints is not empty
        if (waypoints != null && waypoints.Length > 0)
        {
            // Get current waypoint
            Transform point = waypoints[currentIndex];
            // Draw line from position to waypoint
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, point.position);
            // Draw stopping distance sphere
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, stoppingDistance);
            // Draw gravity sphere
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(point.position, gravityDistance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
            currentTask = State.seek;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTask = State.patrol;
        }
    }

    void Seek()
    {
        na.speed = sprintSpeed;
        na.destination = target.transform.position;
    }

    void Patrol()
    {
        na.speed = moveSpeed;

        // 1 - Find waypoint
        Transform point = waypoints[currentIndex];

        // 2 - Calculate distance from waypoint
        float distance = Vector3.Distance(transform.position, point.position);
        
        if (distance < stoppingDistance)
        {

            if (patrolBehaviour == PatrolMethod.loop) // Loop
            {
                currentIndex += 1;
                if (currentIndex >= waypoints.Length)
                {
                    currentIndex = 1;
                    // FIGURE OUT HOW TO REMOVE WAYPOINT 0 FROM LIST
                }
            }
            else if (patrolBehaviour == PatrolMethod.endToEnd) // End to end
            {
                if (endForward)
                {
                    if (currentIndex < waypoints.Length)
                    {
                        currentIndex += 1;
                    }
                    else
                    {
                        endForward = false;
                    }
                }
                else
                {
                    if (currentIndex > 1)
                    {
                        currentIndex -= 1;
                    }
                    else
                    {
                        endForward = true;
                    }
                }
            }
            else if (patrolBehaviour == PatrolMethod.wander) // Wander
            {
                currentIndex = Random.Range(1, waypoints.Length);
                // FIGURE OUT HOW TO REMOVE WAYPOINT 0 FROM LIST
            }




        }

        na.destination = point.position;
    }
}
