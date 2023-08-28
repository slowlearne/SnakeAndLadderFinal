/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Path : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed=2;
    float travelledDistance;
    void Update()
    {
        travelledDistance = travelledDistance + speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(travelledDistance);
        transform.rotation = pathCreator.path.GetRotationAtDistance(travelledDistance);
    }
}
*/
using PathCreation;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    public float waypointRadius = 0.1f;

    private List<Vector3> waypoints;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        waypoints = pathCreator.waypoints;
        transform.position = waypoints[0];
    }

    private void Update()
    {
        Vector3 targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint) < waypointRadius)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}
