using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_battery_bar : MonoBehaviour
{

    public scr_pew_pew_pew pew_pew;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = pew_pew.battery / pew_pew.maxbattery;
    }
}
