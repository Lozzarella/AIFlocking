using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    [System.Serializable]

    public struct BehaviourGroup//a struct is a collection of variables that are related together under one variable
    {
        public FlockBehaviour behaviour;
        public float weights;

    }

    public BehaviourGroup[] behaviours;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 move = Vector2.zero;
        foreach(BehaviourGroup behave in behaviours)//grabbing behave in behaviours
        {
            Vector2 partialMove = behave.behaviour.CalculateMove(agent, context, flock) * behave.weights; //passing through the data and the struct behaviour and multiplying it by the weight
            if (partialMove != Vector2.zero)//grabbing the move that comes from each individual behaviour and combing them together
            {
                if (partialMove.sqrMagnitude > behave.weights * behave.weights)//check to make sure that whatever we get back isn't too large and if it is we need to normalise it and multiply by the weight
                {
                    partialMove.Normalize();
                    partialMove *= behave.weights;
                }
            }
            move += partialMove; //add together
        }
        return move;
    }
}
