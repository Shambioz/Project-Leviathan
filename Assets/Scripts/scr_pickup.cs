using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class scr_pick_up_object : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject carriedObject;
    private GameObject obj_to_duplicate;
    public scr_inventory inventory; // Assign this in the inspector
    public Transform player; // Reference to the player's transform
    public bool is_active = false;
    public GameObject obj_p_copy;
    private Vector3 playerPosition;
    private Vector3 screenPosition;
    private Vector3 originalScale;
    private Vector3 newScale;
    private Vector3 cameraPosition;
    private Vector3 cameraForward;
    private Vector3 centerPosition;

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



        if (GameObject.Find(obj_p_copy.name) != null && is_active == true)
        {
            // Get the player's position in world space
            playerPosition = scr_player_manager.instance.GetPlayerPosition();
            Debug.Log("player position: " + player);
            // Convert the player's position to screen coordinates
            cameraPosition = Camera.main.transform.position;
            cameraForward = Camera.main.transform.forward;

            centerPosition = cameraPosition + cameraForward * 1f;

            // Set the object's position to be in the center of the screen
            obj_p_copy.transform.position = centerPosition;
            Debug.Log("object position: " + screenPosition.x + screenPosition.y + 0f);
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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~2))
            {
                scr_pickupable p = hit.collider.GetComponent<scr_pickupable>();
                if (p != null)
                {
                    obj_to_duplicate = hit.collider.gameObject;
                    obj_p_copy = Instantiate(obj_to_duplicate);
                    obj_p_copy.SetActive(true);
                    Destroy(obj_p_copy.GetComponent<scr_pickupable>());
                    obj_p_copy.layer = 2;
                    is_active = true;
                    // Get the current scale of the object
                    originalScale = transform.localScale;

                    // Scale the object to be 2 times smaller
                    newScale = originalScale * 0.7f;

                    // Apply the new scale to the object
                    obj_p_copy.transform.localScale = newScale;
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