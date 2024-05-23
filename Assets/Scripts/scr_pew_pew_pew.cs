using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scr_pew_pew_pew : MonoBehaviour
{
    public LineRenderer lr;
    private GameObject drone;
    private GameObject playerrro;
    private Vector3 drone_position;
    private Vector3 hit_marker;
    public scr_thief_hit thief_hit;
    public GameObject hitted;
    public scr_pew_pew_pew_2 pew_pew_pew_2;
    public float battery = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        drone = GameObject.Find("Cylinder.001");
        playerrro = GameObject.Find("Main Camera");
        lr.enabled = false;
        battery = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Tell me the truth: " + battery);
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Tell me the truth2: " + battery);
            if (battery > 0f)
            {
                battery -= 0.001f;
                lr.enabled = true;
                drone_position = drone.transform.position;
                hit_marker = playerrro.transform.position + playerrro.transform.forward * 2;
                lr.SetPosition(0, drone_position);
                lr.SetPosition(1, hit_marker);
                int x = Screen.width / 2;
                int y = Screen.height / 2;

                Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10))
                {
                    hitted = hit.collider.GameObject();
                    thief_hit = hitted.GetComponent<scr_thief_hit>();
                    //thief_hit = hit.collider.GameObject().GetComponent< scr_thief_hit >();
                    if (thief_hit != null)
                    {
                        Debug.Log("My GF is cute!");
                        thief_hit.is_hit = true;
                    }
                }
            }
            if (battery <= 0f)
            {
                battery = 0f;
                lr.enabled = false;
            }
        }
        else
        {
            lr.enabled = false;
        }
    }
}