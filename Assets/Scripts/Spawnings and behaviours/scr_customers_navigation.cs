using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_customers_navigation : MonoBehaviour
{
    private bool thief_is_free = true;
    public scr_customers_behaviour scr_customers_behaviour;
    public GameObject targetPoint;
    public Vector3[] coordinates;
    [SerializeField] private NavMeshAgent agent1;
    public scr_thief_ai scr_thief_ai;
    int easy = 0;
    int medium = 2;
    int hard = 4;
    public int cust_amount = 5;
    public GameObject[] Customers;
    public GameObject Customer;
    public GameObject Thief;
    public Vector3 spawn_point = new Vector3(-28.75f, 0.38f, -8.58f);
    public GameObject[] TargetPosition;
    public GameObject[] Artefacts;
    public int count = 0;
    public float thief_chance = 0.9f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
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
        if(count < 5)
        {
            StartCoroutine(SpawnCustomersCoroutine());
        }
        
        Customers = new GameObject[cust_amount];
    }

    // Update is called once per frame
    IEnumerator SpawnCustomersCoroutine()
    {
        while (count < 5)
        {
            Spawning();
            yield return new WaitForSeconds(2f);
        }
        
    }

    public void Spawning()
    {
        bool spawnThief = Random.value < thief_chance;
        Debug.Log(spawnThief);
        if (spawnThief)
        {
            SpawnThief(1);
            
        }
        if (!spawnThief)
        {
            SpawnCustomer(1);
            
        }
        count++;
    }

    public GameObject[] GetTargetPositions()
    {
        return TargetPosition;
    }

    List<GameObject> SpawnCustomer(int amount)
    {
        List<GameObject> customers = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, TargetPosition.Length);
            GameObject RandomPoint = TargetPosition[randomIndex];
            GameObject customer = Instantiate(Customer, spawn_point, Quaternion.identity);
            customer.AddComponent<scr_thief_hit>();
            customer.AddComponent<scr_customers_behaviour>();
            customer.AddComponent<Rigidbody>();
            NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
            customers.Add(customer);
        }

        return customers;
    }
    GameObject SpawnThief(int amount)
    {
            GameObject thief = Instantiate(Thief, spawn_point, Quaternion.identity);
            //thief.AddComponent<Rigidbody>();
            thief.AddComponent<scr_pickupable>();
            //thief.AddComponent<NavMeshAgent>();
            thief.AddComponent<scr_thief_hit>();
            thief.AddComponent<scr_thief_behaviour>();
            return thief;
    }
}
