using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_pew_pew_pew_2 : MonoBehaviour
{
    public float battery;
    public float maxbattery;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (battery > 0)
            {
                battery -= 0.001f;
            }
            if (battery < 0f)
            {
                battery = 0f;
            }
            Debug.Log("bat:" + battery);
            Debug.Log("bat perc: " + battery / maxbattery);
        }
    }
}
