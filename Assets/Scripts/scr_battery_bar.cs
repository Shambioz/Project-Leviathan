using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_battery_bar : MonoBehaviour
{

    public scr_pew_pew_pew pew_pew;
    public Slider slider;
    //public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"thinghu:  {pew_pew.battery / pew_pew.maxbattery}" );
        Debug.Log($"pew_pew.battery: {pew_pew.battery}  pew_pew.maxbattery {pew_pew.maxbattery}" );
        slider.value = pew_pew.battery / pew_pew.maxbattery;
        //fill.fillAmount = pew_pew.battery / pew_pew.maxbattery;
    }
}
