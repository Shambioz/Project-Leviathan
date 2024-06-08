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
    public bool spoke = false;
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
    private float AudioClipLengthEnd = 0;
    public scr_audio_manager scr_audio_manager;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        {
            if (AudioClipLengthEnd !=0 && Time.time >= AudioClipLengthEnd && spoke == true)
            {
                Subtitle.text = "";
                AudioClipLengthEnd = 0;
            }
        }
    }

    void Interact()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left-Click");
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            layerMask = LayerMask.GetMask("Water");
            if (Physics.Raycast(ray, out hit, 10, layerMask))
            {
                obj_interacting = hit.collider.GameObject();
                Debug.Log(obj_interacting.name);
                scr_pickupableInt = obj_interacting.GetComponent<scr_pickupable>();
                if (scr_pickupableInt != null)
                {

                    //stop all audio source in previous array if any
                    
                    if (scr_audio_manager.audioSources != null)
                    {
                         foreach (var AudioSource in scr_audio_manager.audioSources)
                        {
                            if (AudioSource != null)
                            {
                                AudioSource.Stop();
                            }
                        }

                        if (scr_audio_manager.playAudioAndDisplayTextCoroutine != null) { scr_audio_manager.StopPlayAudioAndDisplayTextCoroutine(); }
                        scr_audio_manager.audioSources = scr_pickupableInt.audioSources;
                        scr_audio_manager.displayTexts = scr_pickupableInt.displayTexts;
                        scr_audio_manager.PlayAudioAndDisplayText();

                    }
                }
                
            }
        }

    }

}
