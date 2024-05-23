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

    private enum CustomerState
    {
        Idle,
        Moving,
        Going,
        Waiting
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }

        // Find and get the scr_customers_navigation component
        navigation = FindObjectOfType<scr_customers_navigation>();
        if (navigation == null)
        {
            Debug.LogError("scr_customers_navigation component not found.");
            return;
        }

        currentState = CustomerState.Idle;
        StartCoroutine(StateMachine());
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
            }
        }
    }

    IEnumerator IdleState()
    {
        // Perform idle actions, if any
        // Transition to Moving state
        TransitionToState(CustomerState.Moving);
        yield break;
    }

    IEnumerator MovingState()
    {
        Debug.Log("Transitioning to Moving state");

        // Pick a random target position
        int randomIndex = Random.Range(0, navigation.TargetPosition.Length);
        Vector3 targetPosition = navigation.TargetPosition[randomIndex].transform.position;
        agent.SetDestination(targetPosition);
        agent.stoppingDistance = 4;

        // Transition to Going state
        TransitionToState(CustomerState.Going);
        yield break;
    }

    IEnumerator GoingState()
    {
        Debug.Log("Moving to target");

        while (true)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {   
                TransitionToState(CustomerState.Waiting);
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator WaitingState()
    {
        Debug.Log("Waiting at target");

        // Wait for 5 seconds at the target
        yield return new WaitForSeconds(5f);

        // Transition to Moving state
        TransitionToState(CustomerState.Moving);
    }

    private void TransitionToState(CustomerState newState)
    {
        Debug.Log($"Transitioning to {newState}");
        currentState = newState;
    }
}


