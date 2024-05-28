
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class scr_customers_behaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private scr_customers_navigation navigation;
    private CustomerState currentState;
    private Vector3 exit = new Vector3(8.13f, 9.55f, -11.9f);
    private int count = 0;

    private enum CustomerState
    {
        Idle,
        Moving,
        Going,
        Waiting,
        Exiting
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
            Debug.Log("NavMeshAgent component added");
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        navigation = FindObjectOfType<scr_customers_navigation>();
        rb.mass = 100f; // Increase mass
        rb.angularDrag = 10f; // Increase angular drag
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Freeze rotation on X and Z axis
        if (navigation == null)
        {
            Debug.LogError("Navigation component not found");
            return;
        }
        agent.updateRotation = false;
        currentState = CustomerState.Idle;
        StartCoroutine(StateMachine());
    }

    private void UpdateRotation()
    {
        // Calculate the direction to look at
        if (agent.velocity.sqrMagnitude > 0.1f) // If the agent is moving
        {
            Vector3 direction = agent.velocity.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }
    IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case CustomerState.Idle:
                    yield return StartCoroutine(IdleState());
                    break;
                case CustomerState.Moving:
                    yield return StartCoroutine(MovingState());
                    break;
                case CustomerState.Going:
                    yield return StartCoroutine(GoingState());
                    break;
                case CustomerState.Waiting:
                    yield return StartCoroutine(WaitingState());
                    break;
                case CustomerState.Exiting:
                    yield return StartCoroutine(ExitState());
                    break;
            }
            yield return null;
        }
    }

    IEnumerator IdleState()
    {
        TransitionToState(CustomerState.Moving);
        yield break;
    }

    IEnumerator MovingState()
    {
        int randomIndex = Random.Range(0, navigation.TargetPosition.Length);
        Vector3 targetPosition = navigation.TargetPosition[randomIndex].transform.position;
        agent.SetDestination(targetPosition);
        agent.stoppingDistance = 2;
        TransitionToState(CustomerState.Going);
        yield break;
    }

    IEnumerator GoingState()
    {
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            UpdateRotation();
            yield return null;
        }
        TransitionToState(CustomerState.Waiting);
    }

    IEnumerator WaitingState()
    {
        yield return new WaitForSeconds(5f);
        count++;
        if (count > 5)
        {
            TransitionToState(CustomerState.Exiting);
        }
        else
        {
            TransitionToState(CustomerState.Moving);
        }
    }

    IEnumerator ExitState()
    {
        agent.SetDestination(navigation.spawn_point);
        agent.stoppingDistance = 1;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance || agent.velocity.sqrMagnitude >= 0.1f)
        {
            UpdateRotation();
            yield return null;
        }
        if(navigation.count == 5) { navigation.count--; Destroy(gameObject); }
        
    }

    private void TransitionToState(CustomerState newState)
    {
        currentState = newState;
    }
    
}



