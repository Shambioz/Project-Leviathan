using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class scr_place_correctly : MonoBehaviour
{
    public Collider[] colliders;
    public GameObject[] Artifacts;
    public Vector3[] PointsToPlace;
    public Rigidbody rb;
    public scr_fixing_after_theo_fucked_up_again points; 
    // Start is called before the first frame update
    void Start()
    {
        PointsToPlace = new Vector3[Artifacts.Length];
        for (int i = 0; i < Artifacts.Length; i++)
        {
            PointsToPlace[i] = Artifacts[i].transform.position;
        }
        /*PointsToPlace[1] = new Vector3(-44.6f, 0.808f, 37.92f); //canon
        PointsToPlace[5] = new Vector3(-33.47f, 3.89f, 61); //stone
        PointsToPlace[3] = new Vector3(-44.5f, 1.8f, 89.9f); //first cit
        PointsToPlace[8] = new Vector3(-45.47f, 3.7f, 101.8f); //coins
        PointsToPlace[0] = new Vector3(-41.9f, 1.79f, 113.18f);//armor
        PointsToPlace[9] = new Vector3(-30.8f, 3.7f, 116.24f);//sword
        PointsToPlace[7] = new Vector3(-18.53f, 3.7f, 116.24f);//artefact 4
        PointsToPlace[10] = new Vector3(-11f, 3.7f, 107.84f);//johnny skull
        PointsToPlace[2] = new Vector3(-18.67f, 3.7f, 94.12f);//2
        PointsToPlace[4] = new Vector3(-33.87f, 1.77f, 93.11f);//british
        PointsToPlace[6] = new Vector3(-7.4f, 3.8f, 62.62f);//1*/
        // Assign each Artifact's initial position to PointsToPlace
        /*for (int i = 0; i < Artifacts.Length; i++)
        {
            PointsToPlace[i] = Artifacts[i].transform.position;
        }*/
    }

    void CheckPlacement()
    {
        for (int i = 0; i < Artifacts.Length; i++)
        {
            GameObject artifactBounds = Artifacts[i];
            Collider colliderBounds = colliders[i];
            rb = Artifacts[i].GetComponent<Rigidbody>();

            if (colliderBounds.bounds.Contains(artifactBounds.transform.position) && Artifacts[i].GetComponent<scr_pickupable>().picked == false)
            {
                if(Artifacts[i].GetComponent<scr_pickupable>().isFromThief == true)
                {
                    Debug.Log("Placed");
                    points.artems_points += 5;
                    Artifacts[i].GetComponent<scr_pickupable>().isFromThief = false;
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    Artifacts[i].transform.position = PointsToPlace[i];
                }
                else if(Artifacts[i].GetComponent<scr_pickupable>().isFromThief == false)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    Artifacts[i].transform.position = PointsToPlace[i];

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i].transform.position = PointsToPlace[i];
        }*/
        CheckPlacement();
    }
}
