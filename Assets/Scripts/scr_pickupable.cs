using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class scr_pickupable : MonoBehaviour
{
    public TextMeshProUGUI UItext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //highlight Objects when mouse hovers over obj
    void OnMouseEnter()
    {
        gameObject.layer = 3;
        UItext.text = "" + gameObject.name;
    }

    //unhighlight Objects when mouse hovers over obj
    void OnMouseExit()
    {
        gameObject.layer = 0;
        UItext.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check for a specific collider tag to ensure we're reacting to the correct object
        // For example, if your player has a tag "Player"
        if (other.CompareTag("Player") && Input.GetMouseButton(1))
        {
            gameObject.SetActive(false); // This disables the GameObject the script is attached to
        }
    }
}
