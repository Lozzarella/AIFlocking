using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{   
    public FlockAgent agentPrefab;
    public List<FlockAgent> agents;// = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    [Range(10, 500)]
    public int startingCount = 250;
    public float agentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;


    float _squareMaxSpeed;
    float _squareNeighbourRadius;
    float _sqaureAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return _sqaureAvoidanceRadius;} }


    public void Start()
    {
        _squareMaxSpeed = maxSpeed * maxSpeed; //square rooting the maxSpeed
        _squareNeighbourRadius = neighbourRadius * neighbourRadius;
        _sqaureAvoidanceRadius = _squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * startingCount * agentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)), transform);
            //instantiate creates a clone of gameobject or prefab
            //agentPrefab is the prefab
            //Random.insideUnitCircle gives us a random point within a circle with a radius of one
            //multiple by startingCount and agentDensity for consistency
            newAgent.name = "Agent " + i; //change the name of the prefab
            newAgent.Initialise(this);//this instance of the class flock
            agents.Add(newAgent);//add new agent to the list of agents 

        }
    }

    private void Update()// for physics calculation
    {
        foreach (FlockAgent agent in agents)//looping through each agent
        {
            List<Transform> context = GetNearbyObjects(agent);

            //FOR TESTING
           // agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f); //max out at 6

            Vector2 move = behaviour.CalculateMove(agent, context, this);//using the behaviour to calculate the direction the agent should move in and the speed
            move *= driveFactor;//multiply by speed
            if (move.sqrMagnitude > _squareMaxSpeed)//if the speed is greater than the max speed
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)//other scripts can't call onto it
    {
        List<Transform> context = new List<Transform>();//store any object in the vicinity
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        foreach (Collider2D c in contextColliders)//everything that overlaps goes through this array
        {
            if (c != agent.AgentCollider)//dont want to add itself as a neighbour
            {
                context.Add(c.transform);
            }
        }
        return context; //anything that is an array gets added to the context
    }


}
