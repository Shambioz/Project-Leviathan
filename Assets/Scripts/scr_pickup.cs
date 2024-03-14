using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class scr_pick_up_object : MonoBehaviour
{
    private bool isCarrying = false;
    public scr_inventory inventory; // Assign this in the inspector
    public Transform player; // Reference to the player's transform
    public bool is_active = false;
    private int state = 1;
    public GameObject obj_carried;
    private Vector3 playerPosition;
    private Vector3 screenPosition;
    private Vector3 cameraPosition;
    private Vector3 cameraForward;
    private Vector3 centerPosition;
    public Collider[] colliders;
    public Rigidbody rb;
    public int layerMask;

    void Update()
    {
        Pickup();



        if (GameObject.Find(obj_carried.name) != null && is_active == true)
        {
            // Get the player's position in world space
            playerPosition = scr_player_manager.instance.GetPlayerPosition();
            Debug.Log("player position: " + player);
            // Convert the player's position to screen coordinates
            cameraPosition = Camera.main.transform.position;
            cameraForward = Camera.main.transform.forward;

            centerPosition = cameraPosition + cameraForward * 1f;

            // Set the object's position to be in the center of the screen
            obj_carried.transform.position = centerPosition;
            Debug.Log("object position: " + screenPosition.x + (screenPosition.y-25) + 0f);
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
            if (state == 1)
            {
                layerMask = ~LayerMask.GetMask("Ignore Raycast");
                if (Physics.Raycast(ray, out hit, 10, layerMask))
                {
                    obj_carried = hit.collider.GameObject();
                    if (obj_carried != null)
                    {
                        is_active = true;
                        state = 2;
                        DisableNextFrame(obj_carried);
                    }
                    rb = obj_carried.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.useGravity = false;
                    }
                    colliders = obj_carried.GetComponentsInChildren<Collider>();
                    foreach (Collider collider in colliders)
                    {
                        collider.enabled = false;
                    }
                }
            }
            else if (state == 2)
            {
                rb = obj_carried.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                }
                colliders = obj_carried.GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    collider.enabled = true;
                }
                obj_carried = null;
                is_active = false;
                state = 1;
            }
        }
    }

    IEnumerator DisableNextFrame(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        obj.SetActive(false);
    }

    /*oid CheckDrop()
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
    }*/
}