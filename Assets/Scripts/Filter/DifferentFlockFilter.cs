using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Different Flock")]

public class DifferentFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            if (itemAgent != null)
            {
                if (itemAgent.AgentFlock != agent.AgentFlock)// if the flock is not equal to agent flock
                {
                    filtered.Add(item);
                }
            }
        }
        return filtered;
    }
}
