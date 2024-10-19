using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] Transform player;
    State currentState;
    public Transform safeSpot;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        currentState = new Idle(this.gameObject, agent, animator, player);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
