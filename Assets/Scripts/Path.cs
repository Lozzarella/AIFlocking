using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints;

    public float radius = 2.5f;

    [SerializeField] private Vector3 gizmoSize = Vector3.one;

    public bool isFill = true;

    public void Start()
    {
        FillWithChildren();
    }

    private void FillWithChildren()
    {
        if (!isFill)
        {
            return;
        }
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            if (child != transform)
            {
                waypoints.Add(child);
            }

        }
    }
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count == 0) //if we have no waypoints there is nothing to draw
        {
            return;
        }

        for (int i = 0; i < waypoints.Count; i++)
        {
            Transform waypoint = waypoints[i];
            if (waypoint == null)
            {
                continue;//goes to next loop
            }
            Gizmos.color = Color.cyan;// colour of the line we are drawing
            Gizmos.DrawCube(waypoint.position, gizmoSize);//cube on the way point

            if (i + 1 < waypoints.Count && waypoints[i + 1] != null) // i = whatever waypoint we are currently at //is there a next waypoint after this point and the next one is not null
            {
                Gizmos.DrawLine(waypoint.position, waypoints[i + 1].position); //a line to the next waypoint
            }

        }
    }
}
