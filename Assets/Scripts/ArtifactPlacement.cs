using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPlacement : MonoBehaviour
{
    [SerializeField] Collider ArtifactCollider;
    [SerializeField] scr_fixing_after_theo_fucked_up_again points;
    [SerializeField] Vector3 targetRotation;
    [SerializeField] scr_pickupable scr_Pickupable;

    private Rigidbody rb;

    void Start()
    {
        targetRotation = transform.rotation.eulerAngles;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == ArtifactCollider)
        {
            scr_Pickupable.is_in_place = true;
            SetPlacement();
        }
    }

    void SetPlacement()
    {
        if (GetComponent<scr_pickupable>().isFromThief)
        {
            Debug.Log("Placed");
            points.artems_points += 5;
            GetComponent<scr_pickupable>().isFromThief = false;
            PlaceArtifact();
        }
        else
        {
            PlaceArtifact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == ArtifactCollider)
        {
            scr_Pickupable.is_in_place = false;
        }
    }
    void PlaceArtifact()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.position = ArtifactCollider.transform.position;
        transform.rotation = Quaternion.Euler(targetRotation); // Set the rotation to the target rotation
    }
}
