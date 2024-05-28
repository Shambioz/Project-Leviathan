using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class scr_thief_behaviour : MonoBehaviour
{
    public GameObject inventory;
    private NavMeshAgent agent;
    private ThiefState currentState;
    private scr_customers_navigation navigation;
    private Vector3 exit = new Vector3(8.13f, 9.55f, -11.9f);
    private int count = 0;

    private enum ThiefState
    {
        Idle,
        Moving,
        Going,
        Waiting,
        Stealing,
        Stealing_after_paralising,
        Exiting,
        Finale
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Rigidbody rb = GetComponent<Rigidbody>();
        navigation = FindObjectOfType<scr_customers_navigation>();
        rb.mass = 100f; // Increase mass
        rb.angularDrag = 10f; // Increase angular drag
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Freeze rotation on X and Z axis\
        agent.updateRotation = false;
        agent.speed = 10f;
        currentState = ThiefState.Idle;
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
                case ThiefState.Idle:
                    yield return StartCoroutine(IdleState());
                    break;
                case ThiefState.Moving:
                    yield return StartCoroutine(MovingState());
                    break;
                case ThiefState.Going:
                    yield return StartCoroutine(GoingState());
                    break;
                case ThiefState.Waiting:
                    yield return StartCoroutine(WaitingState());
                    break;
                case ThiefState.Stealing:
                    yield return StartCoroutine(StealingState());
                    break;
                case ThiefState.Stealing_after_paralising:
                    yield return StartCoroutine(Stealing_after_paralising());
                    break;
                case ThiefState.Exiting:
                    yield return StartCoroutine(ExitState());
                    break;
                case ThiefState.Finale:
                    yield return StartCoroutine(FinaleState());
                    break;
            }
            yield return null;
        }
    }

    IEnumerator IdleState()
    {
        TransitionToState(ThiefState.Moving);
        yield break;
    }

    IEnumerator MovingState()
    {
        int randomIndex = Random.Range(0, navigation.TargetPosition.Length);
        Vector3 targetPosition = navigation.TargetPosition[randomIndex].transform.position;
        inventory = navigation.Artefacts[randomIndex];
        agent.SetDestination(targetPosition);
        agent.stoppingDistance = 2;
        TransitionToState(ThiefState.Going);
        yield break;
    }
    IEnumerator GoingState()
    {
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            UpdateRotation();
            yield return null;
        }
        TransitionToState(ThiefState.Waiting);
    }

    IEnumerator WaitingState()
    {
        yield return new WaitForSeconds(5f);
        TransitionToState(ThiefState.Stealing);
    }

    IEnumerator StealingState()
    {
        inventory.transform.SetParent(transform); // Attach to the thief
        inventory.transform.localPosition = Vector3.zero; // Position it correctly on the thief
        Rigidbody rb = inventory.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Make the artifact kinematic to prevent physics issues
        }
        //Destroy(inventory.gameObject);
        yield return new WaitForSeconds(2f);
        TransitionToState(ThiefState.Exiting);
    }

    IEnumerator Stealing_after_paralising()
    {
        //fix checking if artefact's on place
        yield return null;
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
        TransitionToState(ThiefState.Finale);
        yield return null;
        
    }

    IEnumerator FinaleState()
    {
        if (inventory != null)
        {
            inventory.transform.SetParent(null); // Detach from the thief
            Rigidbody rb = inventory.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics
            }
            
        }
        navigation.count--;
        Destroy(gameObject); // Destroy the thief
        yield return null;
    }
    private void TransitionToState(ThiefState newState)
    {
        currentState = newState;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
