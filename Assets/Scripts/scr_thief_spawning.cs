using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;



public class scr_thief_spawning : MonoBehaviour
{
    private bool thief_is_free = true;
    public Vector3[] coordinates;
    public GameObject Thief;
    // Start is called before the first frame update 
    void Start()
    {
        coordinates = new Vector3[4];
        coordinates[0] = new Vector3(-41, 6.5f, 90);
        coordinates[1] = new Vector3(-34, 6.5f, 91);
        coordinates[2] = new Vector3(-15, 6.5f, 108);
        coordinates[3] = new Vector3(-19, 6.5f, 100);
        coordinates[4] = new Vector3(-29.7f, 6.5f, 61);
    }

    // Update is called once per frame 
    void Update()
    {
        if (thief_is_free)
        {
            SpawnThief();
            thief_is_free=false;
            


        }

    }
     GameObject SpawnThief()
    {
        int randomIndex = Random.Range(0, coordinates.Length);
        randomIndex = Random.Range(0, coordinates.Length);
        Vector3 randomVector = coordinates[randomIndex];
        GameObject thief = Instantiate(Thief, randomVector, Quaternion.identity);
        return thief;
    }
}