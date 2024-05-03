using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    // we need to track what waypoints are next to this waypoint

    //list of waypoints that are next to this waypoint
    public List<WayPoint> nextWayPoints = new List<WayPoint>();

    // function to tell us what waypoint are next to this waypoint
    public List<WayPoint> GetNextWayPoints()
    {
        return nextWayPoints;
    }

    
}
