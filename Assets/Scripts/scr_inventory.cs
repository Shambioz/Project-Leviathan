using UnityEngine;

public class Inventory : MonoBehaviour
{
    private scr_pickupable[] items = new scr_pickupable[3];
    private int index = 0;

    public void AddItem(scr_pickupable item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                break;
            }
        }
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
}
