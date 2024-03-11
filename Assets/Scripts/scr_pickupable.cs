using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_pickupable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //highlight Objects when mouse hovers over obj
    void OnMouseEnter()
    {
        gameObject.layer = 3;
    }

    //unhighlight Objects when mouse hovers over obj
    void OnMouseExit()
    {
        gameObject.layer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
