using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_check : MonoBehaviour
{
    public int how_many_you_have_placed = 0;
    private scr_pick_up_object pick_up_object;
    private GameObject the_chosen_one;
    private GameObject the_chosen_one2;
    private GameObject the_chosen_one3;
    private GameObject the_chosen_one4;
    private Collider fun1;
    private Collider fun2;
    private Collider fun3;
    private Collider fun4;
    private Collider fun5;
    private Collider fun6;
    private Collider fun7;
    private Collider fun8;
    public GameObject prefabtoplace1;
    public GameObject prefabtoplace2;
    public GameObject prefabtoplace3;
    public GameObject prefabtoplace4;
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("1234");
        the_chosen_one = GameObject.Find("object_1_spot");
        the_chosen_one2 = GameObject.Find("object2_spot");
        the_chosen_one3 = GameObject.Find("object3_spot");
        the_chosen_one4 = GameObject.Find("object4_spot");

        //the_chosen_one.tag = "obj1spot";
        fun1 = the_chosen_one.GetComponent<Collider>();
        the_chosen_one = GameObject.Find("Frame_circle");
        //the_chosen_one.tag = "obj1spot";
        fun2 = the_chosen_one.GetComponent<Collider>();
        //2nd one
        fun3 = the_chosen_one2.GetComponent<Collider>();
        the_chosen_one2 = GameObject.Find("Frame_square");
        fun4 = the_chosen_one2.GetComponent<Collider>();
        //3rd one
        fun5 = the_chosen_one3.GetComponent<Collider>();
        the_chosen_one3 = GameObject.Find("frame oval");
        fun6 = the_chosen_one3.GetComponent<Collider>();    
        //4th one
        fun7 = the_chosen_one4.GetComponent<Collider>();
        the_chosen_one4 = GameObject.Find("frame rectangle");
        fun8 = the_chosen_one4.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fun1.bounds.Intersects(fun2.bounds))
        {
            layerMask = LayerMask.GetMask("Ignore Raycast");
            the_chosen_one.layer = layerMask;
            the_chosen_one.SetActive(false);
            the_chosen_one = GameObject.Find("object_1_spot");
            the_chosen_one.SetActive(false);
            Instantiate(prefabtoplace1, transform.position, Quaternion.identity);
            how_many_you_have_placed++;
        }
        if (fun3.bounds.Intersects(fun4.bounds))
        {
            layerMask = LayerMask.GetMask("Ignore Raycast");
            the_chosen_one2.layer = layerMask;
            the_chosen_one2.SetActive(false);
            the_chosen_one2 = GameObject.Find("object2_spot");
            the_chosen_one2.SetActive(false);
            Instantiate(prefabtoplace2, transform.position, Quaternion.identity);
            how_many_you_have_placed++;
        }
        if(fun5.bounds.Intersects(fun6.bounds))
        {
            layerMask = LayerMask.GetMask("Ignore Raycast");
            the_chosen_one3.layer = layerMask;
            the_chosen_one3.SetActive(false);
            the_chosen_one3 = GameObject.Find("object3_spot");
            the_chosen_one3.SetActive(false);
            Instantiate(prefabtoplace3, transform.position, Quaternion.identity);
            how_many_you_have_placed++;
        }
        if(fun7.bounds.Intersects(fun8.bounds))
        {
            layerMask = LayerMask.GetMask("Ignore Raycast");
            the_chosen_one4.layer = layerMask;
            the_chosen_one4.SetActive(false);
            the_chosen_one4 = GameObject.Find("object4_spot");
            the_chosen_one4.SetActive(false);
            Instantiate(prefabtoplace4, transform.position, Quaternion.identity);
            how_many_you_have_placed++;
        }
    }
}
