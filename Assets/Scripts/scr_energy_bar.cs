using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_energy_bar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private scr_pew_pew_pew_2 pew_pew_pew;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"thinghu:  {pew_pew_pew.battery / pew_pew_pew.maxbattery}");
        Debug.Log($"pew_pew_pew.battery: {pew_pew_pew.battery}  pew_pew_pew.maxbattery {pew_pew_pew.maxbattery}");
        slider.value = pew_pew_pew.battery / pew_pew_pew.maxbattery;
    }
}
