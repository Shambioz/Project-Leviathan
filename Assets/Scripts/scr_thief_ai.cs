using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class scr_thief_ai : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public int stateagent = 0; //0 - active, 1 - inactive
    private int timerro = 0;



    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && stateagent == 0)
        {
            agent.isStopped = false;
        }
        else if (target != null && stateagent == 1)
        {
            agent.isStopped = true;
            timerro++;
            if (timerro == 2000)
            {
                timerro = 0;
                stateagent = 0;
            }
        }
    }
}
