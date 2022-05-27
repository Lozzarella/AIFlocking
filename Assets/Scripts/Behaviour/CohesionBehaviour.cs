using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)// if there is no neighbours then return zero
        {
            return Vector2.zero;
        }
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = filter == null ? context : filter.Filter(agent, context); //if filter == null, then context, else filters

        int count = 0;
        foreach (Transform item in filteredContext)//for all the neighbours
        {

            //if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= flock.
            //{
            cohesionMove += (Vector2)item.position;
            count++;//add all the positions together

            //}
        }
        if (count != 0)
        {
            cohesionMove /= count;//divid by the count - to find the average position
        }
        //direction from a to be = b - a
        cohesionMove -= (Vector2)agent.transform.position;//direction

        return cohesionMove;

    }

 
}
