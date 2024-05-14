using UnityEngine;
using UnityEngine.AI;

public class scr_thief_ai : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public Transform target;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
