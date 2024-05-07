using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_pew_pew_pew : MonoBehaviour
{
    public LineRenderer lr;
    private GameObject drone;
    private GameObject playerrro;
    private Vector3 drone_position;
    private Vector3 hit_marker;
    public float battery = 1f;
    public float maxbattery = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        drone = GameObject.Find("Cylinder.001");
        playerrro = GameObject.Find("Main Camera");
        lr.enabled = false;
        battery = 1;
        maxbattery = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            lr.enabled = true;
            drone_position = drone.transform.position;
            hit_marker = playerrro.transform.position + playerrro.transform.forward * 2;
            lr.SetPosition(0, drone_position);
            lr.SetPosition(1, hit_marker);
            if (battery  > 0)
            {
                battery -= 0.001f;
            }
            if (battery < 0f)
            {
                battery = 0f;
            }
            Debug.Log("BATERY:" + battery);
            Debug.Log("SHBDYAHSIDJ: " + battery / maxbattery);
        }
        else
        {
            lr.enabled = false;
        }
    }
}
