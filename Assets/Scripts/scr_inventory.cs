using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class scr_inventory : MonoBehaviour
{
}
    /*public bool godhelpmeplz = false;
    public scr_pickupable[] items = new scr_pickupable[3];
    private int index = 0;
    private scr_pick_up_object pick_up_object;
   

    public void AddItem(scr_pickupable item)
    {
            if (items[0] != null)
            {
                Place(items[0]);
                Debug.Log("i'm active");
            }
            items[0] = item;
    }

    public scr_pickupable GetActiveItem()
    {
        return items[index];
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            index++;
            if (index >= items.Length)
            {
                index = 0;
            }
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            index--;
            if (index < 0)
            {
                index = items.Length - 1;
            }
        }
    }

    void OnGUI()
    {
        if (items[index] != null)
        {
            GUI.Label(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), items[index].name);
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
            Destroy(pick_up_object.obj_p_copy);
        }
    }

}
/*using UnityEngine;
using System.Collections;

public class ExampleScript : MonoBehaviour
{
    private bool variableLocked = false;

    private void Start()
    {
        // Example of changing the variable
        ChangeVariableAfterDelay(0.5f);
    }

    private void Update()
    {
        // Example of trying to change the variable
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryChangeVariable();
        }
    }

    private void TryChangeVariable()
    {
        if (!variableLocked)
        {
            // Change the variable
            Debug.Log("Variable changed!");
            variableLocked = true;

            // Start the coroutine to unlock the variable after half a second
            StartCoroutine(UnlockVariableAfterDelay(0.5f));
        }
        else
        {
            Debug.Log("Variable is locked!");
        }
    }

    private IEnumerator UnlockVariableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        variableLocked = false;
    }
}*/