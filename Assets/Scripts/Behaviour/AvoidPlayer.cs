using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoid Player")]
public class AvoidPlayer : FlockBehaviour
{
    private PlayerMovement player;
    public float avoidanceRad = 5f;

    public void FindPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>(); //find object of type 
        }
       
    }

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        FindPlayer();
        if (player == null)
        {
            return Vector2.zero;
        }

        Vector2 avoidanceMove = Vector2.zero;

        if (Vector2.SqrMagnitude(player.transform.position - agent.transform.position) <= avoidanceRad)//smarler than the other radius
        {
            avoidanceMove = (Vector2)(agent.transform.position - player.transform.position);

        }
        


        return avoidanceMove;
    }
}
