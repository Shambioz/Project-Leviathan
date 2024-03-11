using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class scr_pick_up_object : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject carriedObject;
    public scr_inventory inventory; // Assign this in the inspector

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
                    StartCoroutine(DisableNextFrame(p.gameObject));
                }
                inventory.AddItem(p);
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