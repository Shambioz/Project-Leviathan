using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class scr_thief_behaviour : MonoBehaviour
{
    public GameObject inventory;
    private NavMeshAgent agent;
    private ThiefState currentState;
    private scr_customers_navigation navigation;
    private Vector3 exit = new Vector3(8.13f, 9.55f, -11.9f);
    public scr_thief_hit hp;
    public bool is_artefact_stolen = false;
    public bool active = false;
    public float radius = 10f;
    private Animator Walking;
    public Transform centerPoint;
    private scr_day_cycle scr_day_cycle;
    public bool YouLost = false;
    public int cycle;
    public int count = 0;
    public bool check1 = false;
    public bool check2 = true;
    public scr_fixing_after_theo_fucked_up_again points;
    private bool free_points = true;
    private enum ThiefState
    {
        Idle,
        Moving,
        Going,
        Waiting,
        Stealing,
        Paralising,
        Stealing_after_paralising,
        Exiting,
        Finale
    }

    void Start()
    {
        points = FindObjectOfType<scr_fixing_after_theo_fucked_up_again>();
        cycle = UnityEngine.Random.Range(0, 3);
        scr_day_cycle = FindObjectOfType<scr_day_cycle>();
        hp = this.GetComponent<scr_thief_hit>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 7f;
        Rigidbody rb = GetComponent<Rigidbody>();
        navigation = FindObjectOfType<scr_customers_navigation>();
        Walking = GetComponent<Animator>();
        Walking.enabled = false;
        rb.mass = 100f; // Increase mass
        rb.angularDrag = 10f; // Increase angular drag
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // Freeze rotation on X and Z axis
        agent.updateRotation = false;
        currentState = ThiefState.Idle;
        StartCoroutine(StateMachine());
    }

    private void UpdateRotation()
    {
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
            /*
            if (scr_day_cycle.Leave)
            {
                yield return StartCoroutine(ExitState());
            }         
            */
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
                case ThiefState.Paralising:
                    yield return StartCoroutine(ParalisingState());
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
        Debug.Log("Idle");
        TransitionToState(ThiefState.Moving);
        yield break;
    }

    IEnumerator MovingState()
    {
        Debug.Log("Identifying the target");
        //Walking.enabled = true;
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
        Debug.Log("Going to the target");
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            UpdateRotation();
            if (scr_day_cycle.leave == 1)
            {
                TransitionToState(ThiefState.Exiting);
                yield break;
            }
            if (active)
            {
                if (inventory.GetComponent<scr_pickupable>().picked == false && free_points)
                {
                    points.artems_points++;
                    free_points = false;
                }
                    TransitionToState(ThiefState.Paralising);
                yield break;
            }
            yield return null;
        }
        TransitionToState(ThiefState.Waiting);
    }

    IEnumerator WaitingState()
    {
        count++;
        Debug.Log("Waiting");
        //Walking.enabled = false;
        yield return new WaitForSeconds(5f);
        if(count <= cycle)
        {
            TransitionToState(ThiefState.Moving);
            yield break;
        }
        TransitionToState(ThiefState.Stealing);
    }

    IEnumerator StealingState()
    {
        Debug.Log("Stealing");
        Collider collider = inventory.GetComponent<Collider>();
        scr_pickupable remove = inventory.GetComponent<scr_pickupable>();
        if(inventory.GetComponent<scr_pickupable>().picked == false)
        {
            Destroy(collider);
            inventory.transform.SetParent(transform); // Attach to the thief
            inventory.transform.localPosition = new Vector3(-0.058f, 1.38f, 0.506f); // Position it correctly on the thief
            inventory.GetComponent<scr_pickupable>().picked = true;
            Rigidbody rb = inventory.GetComponent<Rigidbody>();
            is_artefact_stolen = true;
            if (rb != null)
            {
                rb.isKinematic = true; // Make the artifact kinematic to prevent physics issues
            }
            yield return new WaitForSeconds(2f);
            TransitionToState(ThiefState.Exiting);
            yield break;
        }
        TransitionToState(ThiefState.Moving);
    }

    IEnumerator ParalisingState()
    {
        //Walking.enabled = false;

        Debug.Log("Paralised");
        if (inventory != null)
        {
            inventory.AddComponent<BoxCollider>();
            inventory.GetComponent<scr_pickupable>().picked = false;
            inventory.GetComponent<scr_pickupable>().isFromThief = true;
            inventory.transform.SetParent(null); // Detach from the thief
            Rigidbody rb = inventory.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
        // Stop the agent
        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(3f);
        hp.hp = 1000;
        active = false;
        if (is_artefact_stolen)
        {
            TransitionToState(ThiefState.Stealing_after_paralising);
        }
        else
        {
            TransitionToState(ThiefState.Moving);
        }
        yield break;
    }

    IEnumerator Stealing_after_paralising()
    {
        while (true)
        {
            centerPoint = inventory.transform;
            float distance = Vector3.Distance(transform.position, centerPoint.position);
            Debug.Log("Stealing after paralising");
            if (scr_day_cycle.leave == 1)
            {
                TransitionToState(ThiefState.Exiting);
                yield break;
            }
            if (distance <= radius && inventory.GetComponent<scr_pickupable>().picked == false)
            {
                Collider collider = inventory.GetComponent<Collider>();
                scr_pickupable remove = inventory.GetComponent<scr_pickupable>();
                inventory.GetComponent<scr_pickupable>().picked = true;
                Destroy(collider);
                inventory.transform.SetParent(transform); // Attach to the thief
                inventory.transform.localPosition = Vector3.zero; // Position it correctly on the thief
                Rigidbody rb = inventory.GetComponent<Rigidbody>();
                is_artefact_stolen = true;
                rb.isKinematic = true; // Make the artifact kinematic to prevent physics issues
                
                Debug.Log("Theo");
                yield return new WaitForSeconds(2f);

            }
            else if (distance > radius || inventory.GetComponent<scr_pickupable>().picked == true)
            {
                inventory = null;
                yield return new WaitForSeconds(2f);

            }
            else
            {
                yield return null;
            }
            TransitionToState(ThiefState.Exiting);
            yield break;
        }



    }

    IEnumerator ExitState()
    {
        //Walking.enabled = true;
        Debug.Log("Leaving");
        agent.SetDestination(navigation.spawn_point);
        agent.stoppingDistance = 1;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance || agent.velocity.sqrMagnitude >= 0.1f)
        {
            UpdateRotation();
            if (active)
            {
                TransitionToState(ThiefState.Paralising);
                yield break;
            }
            yield return null;
        }
        if(inventory != null && inventory.GetComponent<scr_pickupable>().picked == true)
        {
            YouLost = true;
            yield return new WaitForSeconds(2f);
        }
        TransitionToState(ThiefState.Finale);
        yield return null;
    }

    IEnumerator FinaleState()
    {
        //Walking.enabled = false;
        Debug.Log("Finale");
        if (inventory != null)
        {
            Debug.Log(YouLost + " finally");
            inventory.GetComponent<scr_pickupable>().picked = false;
            inventory.AddComponent<BoxCollider>();
            inventory.transform.SetParent(null); // Detach from the thief
            Rigidbody rb = inventory.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics
            }
        }
        navigation.count--;
        navigation.StartSpawning();
        Destroy(gameObject); // Destroy the thief
        yield return null;
    }

    private void TransitionToState(ThiefState newState)
    {
        Debug.Log($"Transitioning to {newState}");
        currentState = newState;
    }

    public void TriggerExit()
    {
        inventory = null;
        if (currentState != ThiefState.Exiting && currentState != ThiefState.Finale)
        {
            Debug.Log("please");
            StopAllCoroutines(); // Stop current coroutines
            StartCoroutine(KickingOut());
        }
    }

    IEnumerator KickingOut()
    {
        //Walking.enabled = true;
        Debug.Log("Kicking");
        agent.SetDestination(navigation.spawn_point);
        agent.stoppingDistance = 1;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance || agent.velocity.sqrMagnitude >= 0.1f)
        {
            UpdateRotation();
            if (active)
            {
                TransitionToState(ThiefState.Paralising);
                yield break;
            }
            yield return null;
        }
        //Walking.enabled = false;
        Debug.Log("Finale");
        if (inventory != null)
        {
            inventory.AddComponent<BoxCollider>();
            inventory.GetComponent<scr_pickupable>().picked = false;
            inventory.transform.SetParent(null); // Detach from the thief
            Rigidbody rb = inventory.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics
            }
        }
        navigation.count--;
        navigation.StartSpawning();
        Destroy(gameObject); // Destroy the thief
        yield return null;
    }

    public void Suicide()
    {
        if (inventory != null)
        {
            inventory.AddComponent<BoxCollider>();
            inventory.GetComponent<scr_pickupable>().picked = true;
            inventory.transform.SetParent(null); // Detach from the thief
            Rigidbody rb = inventory.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics
            }
        }
        Destroy(gameObject);
    }
    void Update()
    {
        if(scr_day_cycle.leave == 1)
        {
            Debug.Log("yeass");
        }
        if (hp.hp == 0)
        {
            active = true;
            Debug.Log("Saki saki fuki fuki");
        }
    }
}