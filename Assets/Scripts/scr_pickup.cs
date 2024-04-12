
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject carriedObject;
    public Inventory inventory; // Assign this in the inspector

    void Update()
    {
        if (isCarrying)
        {
            CheckDrop();
        }
        else
        {
            Pickup();
        }
    }

    void Pickup()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                scr_pickupable p = hit.collider.GetComponent<scr_pickupable>();
                if (p != null)
                {
                    isCarrying = true;
                    carriedObject = p.gameObject;
                    inventory.AddItem(p);
                    StartCoroutine(DisableNextFrame(p.gameObject));
                }
            }
        }
    }

    IEnumerator DisableNextFrame(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        obj.SetActive(false);
    }

    void CheckDrop()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        isCarrying = false;
        carriedObject = null;
    }
}



