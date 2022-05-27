using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/WanderBehaviour")]
public class WanderBehaviour : FilteredFlockBehaviour
{
    #region Varibles 
    public Path _path;//drag the path into here
    int _currentWaypoint = 0;

    //Vector2 _waypointDirection = Vector2.zero; //direction we want to steer the AI in
    #endregion

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)//override from flock behaviour
    {
        if (_path == null)
        {
            FindPath(agent, context);
        }
        return FollowPath(agent);//follows the path we give it
    }

    private Vector2 FollowPath(FlockAgent agent)//follow the path
    {
        if (_path == null)
        {
            return Vector2.zero;
        }
        Vector3 waypointDirection;
        if (WaypointInRadius(agent, _currentWaypoint, out waypointDirection))
        {
            _currentWaypoint++;
            if (_currentWaypoint > -_path.waypoints.Count)
            {
                _currentWaypoint = 0;
            }
            return Vector2.zero;

        }
        return waypointDirection.normalized;
        
        //return (Vector2)(_path.waypoints[_currentWaypoint].position - agent.transform.position).normalized; //return the waypoint direction //normalise the vector to get a magnitude 
    }

    public bool WaypointInRadius(FlockAgent agent, int currentWaypoint, out Vector3 waypointDirection)
    {
        waypointDirection = (Vector2)(_path.waypoints[_currentWaypoint].position - agent.transform.position);
        if (waypointDirection.magnitude < _path.radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void FindPath(FlockAgent agent, List<Transform> context)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context); //filters out paths
        if (filteredContext.Count == 0)
        {
            return;
        }
        int randomPath = Random.Range(0, filteredContext.Count);//select random path
        _path = filteredContext[randomPath].GetComponentInParent<Path>();
    }
}
