using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_receptionist_movement : MonoBehaviour
{
    public GameObject[] transforms;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int index;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            index = Random.Range(0, transforms.Length);
            agent.SetDestination(transforms[index].transform.position);
            agent.speed = 3;
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                UpdateRotation();
                yield return null;
            }
            yield return new WaitForSeconds(5f);
        }
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
