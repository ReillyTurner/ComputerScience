using UnityEngine;

public class PlayerWaypointTracker : MonoBehaviour
{
    // Array to store the positions of all waypoints
    public Transform[] waypoints;

    // Variable to store the index of the last visited waypoint
    private int lastWaypointIndex = -1;

    // Function to update the last visited waypoint
    private void UpdateLastWaypoint()
    {
        // Loop through all waypoints
        for (int i = 0; i < waypoints.Length; i++)
        {
            // Check if the player is close enough to the current waypoint
            if (Vector3.Distance(transform.position, waypoints[i].position) < 1f)
            {
                // Update the last visited waypoint index
                lastWaypointIndex = i;
            }
        }
    }

    // Function to get the last visited waypoint
    public Transform GetLastWaypoint()
    {
        // If lastWaypointIndex is valid, return the corresponding waypoint
        if (lastWaypointIndex != -1)
        {
            return waypoints[lastWaypointIndex];
        }
        else
        {
            // If no waypoint has been visited yet, return null
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Call the function to update the last visited waypoint
        UpdateLastWaypoint();
    }
}
