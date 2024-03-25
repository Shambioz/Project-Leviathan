using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_on_collision_event : MonoBehaviour
{
    public GameObject EffectedObject;
    public TextMeshProUGUI Subtitle;
    private Collider oc;
    public AudioSource Audio1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("How");
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Entered");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
           
            Debug.Log("Entered");
            Audio1.Play();
            if (Audio1.isPlaying) { Subtitle.text = "RAAAAAAG"; }

        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Audio1.Stop();
            Subtitle.text = "";
        }
    }
        

}
