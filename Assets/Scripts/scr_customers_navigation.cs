using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_customers_navigation : MonoBehaviour
{
    public int cust_amount = 5;
    public GameObject[] Customers;
    public GameObject Customer;
    public Vector3 spawn_point = new Vector3(-28.75f, 0.38f, -8.58f);
    public GameObject[] TargetPosition;
    public int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCustomersCoroutine());
        Customers = new GameObject[cust_amount];
    }

    // Update is called once per frame
    IEnumerator SpawnCustomersCoroutine()
    {
        while (count < 5)
        {
            SpawnCustomer(1);
            count++;
            yield return new WaitForSeconds(2f);
        }
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
            NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
            customers.Add(customer);
        }

        return customers;
    }
}
