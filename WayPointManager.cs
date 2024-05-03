using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    // list of all waypoints in the scene
    public List<WayPoint> wayPoints = new List<WayPoint>();

    // function to get the closest waypoint to a position
    public WayPoint GetClosestWayPoint(Vector3 position)
    {
        // the closest waypoint to the position
        WayPoint closestWayPoint = null;
        // the distance to the closest waypoint
        float closestDistance = Mathf.Infinity;

        // loop through all waypoints
        foreach (WayPoint wayPoint in wayPoints)
        {
            // get the distance to the waypoint
            float distance = Vector3.Distance(position, wayPoint.transform.position);
            // if the distance is less than the closest distance
            if (distance < closestDistance)
            {
                // set the closest waypoint to the waypoint
                closestWayPoint = wayPoint;
                // set the closest distance to the distance
                closestDistance = distance;
            }
        }

        // return the closest waypoint
        return closestWayPoint;
    }
}
