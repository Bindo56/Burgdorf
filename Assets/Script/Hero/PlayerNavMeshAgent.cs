using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMeshAgent : MonoBehaviour
{
    [SerializeField] Transform destination;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        agent.destination = destination.position;
    }

}
