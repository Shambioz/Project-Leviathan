using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class scr_interact_object : MonoBehaviour
{

    public bool is_carrying = false;
    public scr_inventory inventory; // Assign this in the inspector
    public Transform player; // Reference to the player's transform
    public bool is_active = false;
    private GameObject obj_interacting;
    public Collider[] colliders;
    public Rigidbody rb;
    public int layerMask;
    //play sound of object carried
    public scr_pickupable scr_pickupable;
    private scr_pickupable scr_pickupableInt;
    //Add subtitles
    public TextMeshProUGUI Subtitle;
    public GameObject the_one_you_picked_up;
    private float AudioClipLength;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        {
            if (!scr_pickupableInt.audioSource.isPlaying)
            {
                Subtitle.text = "";
            }
        }
    }

    void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left-Click");
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            layerMask = ~LayerMask.GetMask("Ignore Raycast");
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                obj_interacting = hit.collider.GameObject();
                Debug.Log(obj_interacting.name);
                scr_pickupableInt = obj_interacting.GetComponent<scr_pickupable>();
                if (scr_pickupableInt != null && scr_pickupableInt.audioSource.isPlaying)
                {
                    scr_pickupableInt.audioSource.Stop();
                    Subtitle.text = null;
                }
                
                if (scr_pickupableInt != null)
                {

                    if (scr_pickupableInt.audioSource != null)
                    {
                        //play audio from scr_pickupable
                        AudioClipLength = scr_pickupableInt.audioSource.clip.length;
                        scr_pickupableInt.audioSource.Play();
                        if (scr_pickupableInt.audioSource.isPlaying)
                        {
                            Subtitle.text = scr_pickupableInt.audioTranscript;
                        }

                    }

                }
                
            }
        }

    }

}
