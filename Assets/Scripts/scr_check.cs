using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_check : MonoBehaviour
{
    public int how_many_you_have_placed = 0;
    private scr_pick_up_object pick_up_object;
    private GameObject the_chosen_one;
    private Collider fun1;
    private Collider fun2;
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("1234");
        the_chosen_one = GameObject.Find("object_1_spot");
        //the_chosen_one.tag = "obj1spot";
        fun1 = the_chosen_one.GetComponent<Collider>();
        the_chosen_one = GameObject.Find("CanonDick");
        //the_chosen_one.tag = "obj1spot";
        fun2 = the_chosen_one.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fun1.bounds.Intersects(fun2.bounds))
        {
            layerMask = LayerMask.GetMask("Ignore Raycast");
            the_chosen_one.layer = layerMask;
            the_chosen_one.SetActive(false);
            how_many_you_have_placed++;
        }
    }
}
