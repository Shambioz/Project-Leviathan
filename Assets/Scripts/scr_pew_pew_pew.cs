using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_pew_pew_pew : MonoBehaviour
{

    public int layer_mask;

    public LineRenderer line_renderer;

    // Start is called before the first frame update
    void Start()
    {
        line_renderer = this.GetComponent<LineRenderer>();
        line_renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            line_renderer.enabled = true;

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            layer_mask = ~LayerMask.GetMask("Ignore Raycast");


            if (Physics.Raycast(ray, out hit, 10))
            {
                line_renderer.SetPositions(new Vector3[] { ray.origin, hit.point });
            }
        }
        else
        {
            line_renderer.enabled = false;
        }
    }
}
