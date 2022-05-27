using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour //inherenting from flock behaviour
{
    [SerializeField] private Vector2 centre;
    [SerializeField] private float _radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //direction to the center
        Vector2 centerOffset = centre - (Vector2)agent.transform.position;

        float t = centerOffset.magnitude / _radius; //if the value is between 1 and 0
        if (t < 0.9f)//if we are between the centre and 90% of the radius  - within the last 10% draws us to the centre
        {
            return Vector2.zero;

        }
        return centerOffset * t * t;

    }

   
}
