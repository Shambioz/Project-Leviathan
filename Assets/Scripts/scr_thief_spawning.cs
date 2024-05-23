using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;



public class scr_thief_spawning : MonoBehaviour
{
    private bool thief_is_free = true;
    public GameObject targetPoint;
    //private scr_thief_ai agent;
    public Vector3[] coordinates;
    [SerializeField] private NavMeshAgent agent1;
    public GameObject Thief;
    public scr_thief_ai scr_thief_ai;
    int easy = 0;
    int medium = 2;
    int hard = 4;

    public Transform target;
    // Start is called before the first frame update 
    void Start()
    {
        //agent = targetPoint.GetComponent<scr_thief_ai>();
        coordinates = new Vector3[9];
        coordinates[0] = new Vector3(-18.38f, 6.5f, 108);
        coordinates[1] = new Vector3(-33.6f, 6.5f, 89.7f);
        coordinates[2] = new Vector3(-40.6f, 6.5f, 89.7f);
        coordinates[3] = new Vector3(-31.14f, 6.5f, 113);
        coordinates[4] = new Vector3(-41.8f, 6.5f, 109.2f);
        coordinates[5] = new Vector3(-18.5f, 6.5f, 101);
        coordinates[6] = new Vector3(-2.8f, 6.5f, 63.3f);
        coordinates[7] = new Vector3(-27.6f, 6.5f, 60.8f);
        coordinates[8] = new Vector3(-40.6f, 6.5f, 38.3f);
    }

    // Update is called once per frame 
    void Update()
    {
        if (thief_is_free)
        {
            SpawnThief(easy);
            thief_is_free = false;




        }

    }
    List<GameObject> SpawnThief(int amount)
    {
        List<GameObject> thieves = new List<GameObject>();

        for (int i = 0; i < amount + 1; i++)
        {
            int randomIndex = Random.Range(0, coordinates.Length);
            randomIndex = Random.Range(0, coordinates.Length);
            Vector3 randomVector = coordinates[randomIndex];
            GameObject thief = Instantiate(Thief, randomVector, Quaternion.identity);
            thief.AddComponent<Rigidbody>();
            thief.AddComponent<scr_pickupable>();
            thief.AddComponent<NavMeshAgent>();
            thief.AddComponent<scr_thief_hit>();
            NavMeshAgent agent;
            agent = thief.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);
            thief_is_free = false;
            NavMeshAgent agent1 = thief.GetComponent<NavMeshAgent>();

            /*if (target != null)
            {
                agent.SetDestination(target.position);
            }*/
            thieves.Add(thief);
        }
        return thieves;
    }
    void MoveThief()
    {
        if (target != null)
        {

        }
    }
}