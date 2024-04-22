using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_pew_pew_pew : MonoBehaviour
{

    public int layer_mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            layer_mask = ~LayerMask.GetMask("Ignore Raycast");

            Physics.Raycast(ray, out hit, 10, layer_mask);
            Debug.DrawRay(new Vector3(x, y), transform.forward * 10, Color.blue);

            if (Physics.Raycast(ray, out hit, 10, layer_mask))
            {

            }
        }
    }
}
