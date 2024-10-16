using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;

    float sm;

    float detectionRadius = 5;

    float fleeRadius = 10;

    void Start() {

        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        goalLocations = GameObject.FindGameObjectsWithTag("goal");

        int i = Random.Range(0, goalLocations.Length);

        agent.SetDestination(goalLocations[i].transform.position);

        anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));

        ResetAgent();
    }

    void ResetAgent()
    {
        agent.speed = 2;

        sm = Random.Range(0.1f, 1.0f);

        anim.SetFloat("speedMult", sm);

        agent.speed *= sm;

        anim.SetTrigger("isWalking");

        agent.angularSpeed = 120;

        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;

            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();

            agent.CalculatePath(newGoal, path);

            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);

                anim.SetTrigger("isRunning");

                agent.speed = 10;

                agent.angularSpeed = 500;
            }
        }
    }

    void Update() {
        if(agent.remainingDistance < 1)
        {
            ResetAgent();

            int i = Random.Range(0, goalLocations.Length);

            agent.SetDestination(goalLocations[i].transform.position);
        }
    }
}