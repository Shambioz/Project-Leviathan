using UnityEngine;
using UnityEngine.AI;

public class scr_thief_ai : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public int stateagent = 0; //0 - active, 1 - inactive



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && stateagent == 0)
        {
            agent.SetDestination(target.position);
        }
    }
}
