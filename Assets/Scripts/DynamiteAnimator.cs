using UnityEngine;
using UnityEngine.AI;

public class DynamiteAnimator : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (anim == null || agent == null) return;

        
        Vector2 vel = agent.velocity;


        
        anim.SetFloat("tntx", vel.x);
        anim.SetFloat("tnty", vel.y);
    }
}