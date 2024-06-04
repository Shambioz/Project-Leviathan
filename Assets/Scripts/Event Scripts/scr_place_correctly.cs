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

        // Assign each Artifact's initial position to PointsToPlace
        for (int i = 0; i < Artifacts.Length; i++)
        {
            PointsToPlace[i] = Artifacts[i].transform.position;
        }
    }

    void CheckPlacement()
    {
        for (int i = 0; i < Artifacts.Length; i++)
        {
            Bounds artifactBounds = Artifacts[i].GetComponent<Collider>().bounds;
            Bounds colliderBounds = colliders[i].bounds;
            rb = Artifacts[i].GetComponent<Rigidbody>();

            if (artifactBounds.Intersects(colliderBounds) && Artifacts[i].GetComponent<scr_pickupable>().picked == false)
            {
                Debug.Log("Placed");
                if(Artifacts[i].GetComponent<scr_pickupable>().isFromThief == true)
                {
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
        CheckPlacement();
    }
}
