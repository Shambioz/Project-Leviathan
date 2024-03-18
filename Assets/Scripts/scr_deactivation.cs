using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_deactivation : MonoBehaviour
{

    private scr_pickupable pickupable;
    private scr_place_objects place_obj;
    private GameObject the_chosen_one;

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("The name of the aysvdjs" + place_obj.the_name);
        if (place_obj.the_name != null)
        {
            foreach (Transform child in transform)
            {
                //Debug.Log(place_obj.the_name);
                //Debug.Log(place_obj.the_name.Replace("_place", ""));
                Debug.Log("IMA WORKIN");
                if (child.gameObject.name == place_obj.the_name)
                {
                    the_chosen_one = GameObject.Find(place_obj.the_name.Replace("_place", ""));
                    Debug.Log(the_chosen_one.name);
                    pickupable = the_chosen_one.GetComponent<scr_pickupable>();
                    pickupable.is_in_place = true;
                }
            }
        }
    }
}
