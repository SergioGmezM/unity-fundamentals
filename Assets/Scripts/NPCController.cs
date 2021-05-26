using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10.0f;
    public float aggroRange = 10.0f;

    public Transform[] waypoints;

    private int index;
    private float agentSpeed;
    private Transform playerTransform;

    private Animator anim;
    private NavMeshAgent agent;

    private int speedHash;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) agentSpeed = agent.speed;
        index = Random.Range(0, waypoints.Length);
        speedHash = Animator.StringToHash("Speed");
    }

    private void Start()
    {
        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }
    }

    private void Update()
    {
        anim.SetFloat(speedHash, agent.velocity.magnitude);
    }

    private void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    private void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (playerTransform != null && Vector3.Distance(agent.transform.position, playerTransform.position) < aggroRange)
        {
            agent.speed = agentSpeed;
            agent.destination = playerTransform.position;
        }
    }
}
