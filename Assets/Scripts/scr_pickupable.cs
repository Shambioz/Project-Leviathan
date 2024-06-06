using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class scr_pickupable : MonoBehaviour
{
    public TextMeshProUGUI UItext;
    public GameObject ChildLayer;
    public AudioSource audioSource;
    public AudioSource audioSourceInt;
    public string UniqueIdentifier;
    public bool picked;
    public bool isFromThief = false;

    // Array to hold the audio sources
    public AudioSource[] audioSources;

    // Array to hold the strings to be displayed
    public string[] displayTexts;

    //public MeshRenderer ChildMesh;
    //public Material Mat;
    //public SurfaceDataAttributes surfaceData;
    //public Material transparentMaterial;


    // private Color transparentColor = new Color(204, 204, 204, 150); // Use the respective rgba values you want
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //highlight Objects when mouse hovers over obj
    void OnMouseEnter()
    {

        if (gameObject.layer == 0)
        {
            gameObject.layer = 3;
            UItext.text = "" + gameObject.name;
            ChildLayer.layer = 3;
        }
        else if (is_in_place)
        {
            gameObject.layer = 4;
            ChildLayer.layer = 4;
        }
        UItext.text = "" + gameObject.name;
        //Mat = ChildMesh.materials[0];
        //MeshRenderer renderer = ChildMesh.GetComponent<MeshRenderer>();
        //Material originalMat = renderer.sharedMaterial;
        //Material materialTrans = new Material(transparentMaterial);
        //materialTrans.CopyPropertiesFromMaterial(originalMat);
        // renderer.sharedMaterial = materialTrans;
        //ChildMesh.material = transparentMaterial;
        //audioSource.Play();
    }

    //unhighlight Objects when mouse hovers over obj
    void OnMouseExit()
    {
        if (gameObject.layer != 4)
        {
            gameObject.layer = 0;
            UItext.text = "";
            ChildLayer.layer = 0;
        }
        //ChildMesh.material = Mat;
    }

    // Update is called once per frame
    
    /*private void OnTriggerEnter(Collider other)
    {
        // Check for a specific collider tag to ensure we're reacting to the correct object
        // For example, if your player has a tag "Player"
        if (other.CompareTag("Player") && Input.GetMouseButton(1))
        {
            gameObject.SetActive(false); // This disables the GameObject the script is attached to
        }
    }*/



    //Przemek's stuff


    public bool is_in_place = false;


    void Update()
    {
        if (is_in_place == true)
        {
            gameObject.layer = 4;
            ChildLayer.layer = 4;
            this.gameObject.layer = LayerMask.NameToLayer("Water");
        }
    }


}
