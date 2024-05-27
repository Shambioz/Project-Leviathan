
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
        navigation = FindObjectOfType<scr_customers_navigation>();
        if (navigation == null)
        {
            Debug.LogError("Navigation component not found");
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
            yield return null;
        }
        navigation.count--;
        Destroy(gameObject, 2);
    }

    private void TransitionToState(CustomerState newState)
    {
        currentState = newState;
    }
    
}



