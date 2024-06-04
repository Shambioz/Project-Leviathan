
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class scr_customers_behaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private scr_customers_navigation navigation;
    private CustomerState currentState;
    private Vector3 exit = new Vector3(8.13f, 9.55f, -11.9f);
    private int count = 0;
    public scr_thief_hit hp;
    private bool active = false;
    private scr_day_cycle Leave;
    public int cycle;
    public bool Leave1 = false;
    private Animator Walking;
    public scr_fixing_after_theo_fucked_up_again points;
    private bool free_points = true;


    private enum CustomerState
    {
        Idle,
        Moving,
        Going,
        Waiting,
        Paralising,
        Exiting,
        Finale
    }

    // Start is called before the first frame update
    void Start()
    {
        points = FindObjectOfType<scr_fixing_after_theo_fucked_up_again>();
        Walking = GetComponent<Animator>();
        Walking.enabled = false;
        cycle = UnityEngine.Random.Range(1, 5);
        hp = this.GetComponent<scr_thief_hit>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 7f;
        Leave = FindObjectOfType<scr_day_cycle>();
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
    public void TriggerExit()
    {
        if (currentState != CustomerState.Exiting && currentState != CustomerState.Finale)
        {
            StopAllCoroutines(); // Stop current coroutines
            StartCoroutine(KickingOut());
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
                case CustomerState.Finale:
                    yield return StartCoroutine(FinaleState());
                    break;
                case CustomerState.Paralising:
                    yield return StartCoroutine(ParalisingState());
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
        int randomIndex = UnityEngine.Random.Range(0, navigation.TargetPosition.Length);
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
            if (Leave.leave == 1)
            {
                Debug.Log("Close");
                TransitionToState(CustomerState.Exiting);
                yield break;
            }
            if (active)
            {
                Debug.Log("Ivanus");
                TransitionToState(CustomerState.Paralising);
                yield break;
            }
            yield return null;
        }
        TransitionToState(CustomerState.Waiting);
    }

    IEnumerator WaitingState()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(5f);
        count++;
        if (Leave.leave == 1)
        {
            Debug.Log("1");
            TransitionToState(CustomerState.Exiting);
            yield break;
        }
        if (active)
        {
            Debug.Log("2");
            TransitionToState(CustomerState.Paralising);
            yield break;
        }
        if (count > cycle)
        {
            Debug.Log("3");
            TransitionToState(CustomerState.Exiting);
        }
        else
        {
            TransitionToState(CustomerState.Moving);
        }
    }

    IEnumerator ParalisingState()
    {
        Debug.Log("Paralised");
        points.artems_points = points.artems_points - 2;
        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(3f);
        hp.hp = 1000;
        active = false;
        TransitionToState(CustomerState.Exiting);
        yield break;

    }

    IEnumerator ExitState()
    {
        Debug.Log("Exiting");
        agent.SetDestination(navigation.spawn_point);
        agent.stoppingDistance = 1;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance || agent.velocity.sqrMagnitude >= 0.1f)
        {
            UpdateRotation();
            if (active)
            {
                TransitionToState(CustomerState.Paralising);
                yield break;
            }
            yield return null;
        }
        TransitionToState(CustomerState.Finale);
        yield return null;

    }

    IEnumerator FinaleState()
    {
        navigation.count--;
        navigation.StartSpawning();
        Destroy(gameObject); // Destroy the customer
        yield return null;
    }

    private void TransitionToState(CustomerState newState)
    {
        currentState = newState;
    }

    IEnumerator KickingOut()
    {
        Debug.Log("Kicking");
        agent.SetDestination(navigation.spawn_point);
        agent.stoppingDistance = 1;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance || agent.velocity.sqrMagnitude >= 0.1f)
        {
            UpdateRotation();
            if (active)
            {
                TransitionToState(CustomerState.Paralising);
                yield break;
            }
            yield return null;
        }
        navigation.count--;
        navigation.StartSpawning();
        Destroy(gameObject); // Destroy the customer
        yield return null;
    }

    public void Suicide()
    {
        Destroy(gameObject);
    }
        void Update()
    {

        if (hp.hp == 0)
        {
            active = true;
            Debug.Log("Saki saki fuki fuki");
        }
    }
}



