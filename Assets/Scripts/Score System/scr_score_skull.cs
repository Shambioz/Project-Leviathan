using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_score_skull : MonoBehaviour
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

    private void OnTriggerStay(Collider other)
    {
        compareEventbox = other.GetComponent<Collider>();
        if (compareEventbox = thiefcollider)
        {
            scr_score_control.Skull = 500;
            buton = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (buton == true)
        {
            scr_score_control.Skull = 0;
            buton = false;
        }
    }
}
