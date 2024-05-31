using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scr_alter_score : MonoBehaviour
{

    public Collider thiefcollider;
    public Collider eventbox;
    public Collider compareEventbox;
    public scr_score_control scr_score_control;
    private bool buton = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /*void Update()
    {
        if (gameObject.GetComponent<Collider>().bounds.Intersects(thiefcollider.bounds))
        {
            scr_score_control.mortar = 500;
        }

        if (!gameObject.GetComponent<Collider>().bounds.Intersects(thiefcollider.bounds))
        {
            scr_score_control.mortar = 0;scr_score_control.mortar = 500;scr_score_control.mortar = 500;scr_score_control.mortar = 500;scr_score_control.mortar = 500;scr_score_control.mortar = 500;scr_score_control.mortar = 500;  /      }
    */
    private void OnTriggerStay(Collider other)
    {
        compareEventbox = other.GetComponent<Collider>();
        if (compareEventbox = thiefcollider)
        {
            scr_score_control.mortar = 500;
            buton = true;
        }   
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (buton == true)
        {
            scr_score_control.mortar = 0;
            buton = false;
        }
    }
}
