using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_place_objects : MonoBehaviour
{

    public string the_name;
    private scr_pick_up_object pick_up_object;
    private int x = 0;
    private int y = 0;
    public int layerMask;
    private int abcd = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pick_up_object.is_carrying == true)
        {
            x = Screen.width / 2;
            y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            layerMask = ~LayerMask.GetMask("Ignore Raycast");
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                the_name = hit.collider.gameObject.name;
                the_name = the_name + "_place";
            }
        }
        else if (abcd < 100)
        {
            abcd++;
        }
        else
        {
            the_name = null;
        }
    }
}
