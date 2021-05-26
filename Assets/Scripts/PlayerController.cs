using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private int speedHash;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        speedHash = Animator.StringToHash("Speed");
    }

    private void Update()
    {
        anim.SetFloat(speedHash, agent.velocity.magnitude);
    }
}
