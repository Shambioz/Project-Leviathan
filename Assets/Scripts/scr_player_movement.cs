using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_player_movement : MonoBehaviour
{
   

   
    private float lmb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            
            lmb = 2.5f;
        }
        else if (!Input.GetMouseButton(0))
        {

            lmb = 0f;
        }


        // 5
        this.transform.Translate(Vector3.forward * lmb  * Time.deltaTime);
       
    }
}
