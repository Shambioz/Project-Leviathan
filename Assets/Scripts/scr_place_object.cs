using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public Inventory inventory; // Assign this in the inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // "E" key
        {
            scr_pickupable item = inventory.GetActiveItem();
            if (item != null)
            {
                Debug.Log("works");
                Place(item);
            }
        }
    }

    void Place(scr_pickupable item)
    {
        // Create a raycast from the center of the screen
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        // If the raycast hits a surface, place the item at that position
        if (Physics.Raycast(ray, out hit))
        {
            item.gameObject.transform.position = hit.point;
            item.gameObject.SetActive(true);
        }
    }
}
