using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform waypointParent;
    public float moveSpeed = 2f;
    public float stoppingDistance = 1f;
    public float gravityDistance = 2f;
    Rigidbody rb;

    public enum PatrolMethod
    {
        loop,
        endToEnd,
        wander
    }

    bool endForward = true;

    public PatrolMethod patrolBehaviour;

    private Transform[] waypoints; //Array of different scripts
    private int currentIndex = 1;



    // Use this for initialization
    void Start()
    {
        //waypoints = GameObject.Find(waypointParent.G);

        rb = GetComponent<Rigidbody>();

        waypoints = waypointParent.GetComponentsInChildren<Transform>(); // Obtains Transform script info from all child objects of 'waypointParent', which are the waypoints. You can have multiple arrays and versions of this command to obtain multiple different scipts from a set of objects.
    }

    // Update is called once per frame
    void Update()
    {

        Patrol();
        //AirSupply -= BreatheRate * Time.deltaTime;
        //print(Mathf.Round(AirSupply));

        print(waypoints.Length);
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

    void Patrol()
    {
        // 1 - Find waypoint

        Transform point = waypoints[currentIndex];


        // 2 - Calculate distance from waypoint

        float distance = Vector3.Distance(transform.position, point.position);

        // 2.1 - If distance is less than gravity distance
        if (distance < gravityDistance)
        {
            // Turn gravity off
            rb.useGravity = false;
        }
        else // Otherwise
        {
            // Turn gravity on
            rb.useGravity = true;
        }

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

        // Translate enemy towards current waypoint

        transform.position = Vector3.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
        transform.LookAt(point.position);
    }
}
