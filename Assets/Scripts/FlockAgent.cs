using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class FlockAgent : MonoBehaviour
{
    Flock _agentFlock;//this is the flock the agent belongs to
    public Flock AgentFlock { get => _agentFlock; }
    

    private Collider2D _agentCollider;
    public Collider2D AgentCollider
    {
        get { return _agentCollider; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _agentCollider = GetComponent<Collider2D>();
        // we could do some error checking here
        if (_agentCollider == null)
        {
            Debug.Log("Agent does not have collider");
        }
    }

    public void Initialise (Flock flock)
    {
        _agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity.normalized; //changing what up is for the AI and indirectly rotate the AI
        transform.position +=(Vector3)velocity * Time.deltaTime; //move the AI
    }


}
