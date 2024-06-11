using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject[] People;
    public GameObject pos;
    public Vector3 spawn_point;
    public GameObject[] TargetPosition;
    public GameObject[] Artefacts;
    public int count = 0;
    public static float thief_chance;
    public Transform target;
    public bool CanSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        spawn_point = pos.transform.position;
        count = 0;
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

        StartSpawning();
        Customers = new GameObject[cust_amount];
    }

    public void StartSpawning()
        {
        if (count < 5 && CanSpawn)
        {
            StartCoroutine(SpawnCustomersCoroutine());
        }
    }

    // Update is called once per frame
    IEnumerator SpawnCustomersCoroutine()
    {
        while (count < 5)
        {
            yield return new WaitForSeconds(2f);
            Spawning();
            Debug.Log(count);
        }
        
    }

    public void Spawning()
    {
            Debug.Log(count);
            bool spawnThief = Random.value < thief_chance;
            Debug.Log(spawnThief);
            if (spawnThief)
            {
                SpawnThief(1);

            }
            else if (!spawnThief)
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
            int randomSkin = Random.Range(0, People.Length);
            GameObject RandomPerson = People[randomSkin];
            GameObject customer = Instantiate(People[randomSkin], spawn_point, Quaternion.identity);
            customer.AddComponent<scr_thief_hit>();
            customer.AddComponent<scr_customers_behaviour>();
            customer.AddComponent<scr_pew_pew_pew>();
            customers.Add(customer);
        }

        return customers;
    }
    GameObject SpawnThief(int amount)
    {
        int randomSkin = Random.Range(0, People.Length);
        GameObject RandomPerson = People[randomSkin];
        GameObject thief = Instantiate(People[randomSkin], spawn_point, Quaternion.identity);
            //thief.AddComponent<Rigidbody>();
            thief.AddComponent<scr_thief_hit>();
            thief.AddComponent<scr_thief_behaviour>();
            thief.AddComponent<scr_pew_pew_pew>();
            return thief;
    }
}
