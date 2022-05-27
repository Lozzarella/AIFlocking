using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidant")]
public class AvoidantBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)// if there is no neighbours then return zero
        {
            return Vector2.zero;
        }

        Vector2 avoidanceMove = Vector2.zero;
        List<Transform> filteredContext = filter == null ? context : filter.Filter(agent, context);
        int count = 0;

        foreach (Transform item in filteredContext)//for all the neighbours
        {

           if (Vector2.SqrMagnitude(item.position - agent.transform.position) <= flock.SquareAvoidanceRadius)//smarler than the other radius
           {
            avoidanceMove += (Vector2)(agent.transform.position - item.position);
            count++;//add all the positions together

           }
        }

        if (count != 0)
        {
            avoidanceMove /= count;//divid by the count - to find the average position
        }

        return avoidanceMove;
    }
}
