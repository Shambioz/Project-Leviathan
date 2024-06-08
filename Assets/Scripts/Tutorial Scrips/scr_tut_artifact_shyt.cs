using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_tut_artifact_shyt : MonoBehaviour
{
    public GameObject thief;
    public scr_tut_thief_shyt thief_shyt;
    public bool something = false;
    public scr_pickupable pickupable; 


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (thief_shyt.shot_or_not == false)
        {
            this.transform.position = thief.transform.position;
        }
        else if (thief_shyt.shot_or_not == true && something == false)
        {
            something = true;
            this.gameObject.layer = 0;
            this.transform.position += new Vector3(0, 2, 0);
        }
    }
}
