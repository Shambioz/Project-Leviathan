using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

public class scr_pick_up_object : MonoBehaviour
{
    private bool isCarrying = false;
    public scr_inventory inventory; // Assign this in the inspector
    public Transform player; // Reference to the player's transform
    public bool is_active = false;
    private int state = 1;
    private GameObject obj_carried;
    private Vector3 playerPosition;
    private Vector3 screenPosition;
    private Vector3 cameraPosition;
    private Vector3 cameraForward;
    private Vector3 centerPosition;
    public Collider[] colliders;
    public Rigidbody rb;
    public int layerMask;
    //Make pickup obj Transparent
    public Material transparentMaterial;
    private Material Mat;
    private Material ParentMat;
    private MeshRenderer ChildMesh;
    private Transform Child;
    private MeshRenderer ParentMesh;
    //play sound of object carried
    private MonoBehaviour objPickupScr;
    private AudioSource objPickupAudio;
    private AudioSource objPickupAudio2;
    public scr_pickupable scr_pickupable;
    //Add subtitles
    public TextMeshProUGUI Subtitle;
    //Stuff
    private GameObject obj_last_looked;
    private GameObject obj_Looking;
    private scr_pickupable scr_pickupable2;
    private scr_pickupable scr_pickupable3;
    public scr_player_movement scr_player_movement;
    private int switcharoo;
    private float time;
    private int m_MyVar = 0;
    public int myVar = 0;

    void Start() 
    {
        obj_Looking = null;
        scr_player_movement = gameObject.GetComponent<scr_player_movement>();
    }
    
    void Update()
    {
        Pickup();
        {
            // Get the player's position in world space
            playerPosition = scr_player_manager.instance.GetPlayerPosition();
            Debug.Log("player position: " + player);
            // Convert the player's position to screen coordinates
            cameraPosition = Camera.main.transform.position;
            cameraForward = Camera.main.transform.forward;

            centerPosition = cameraPosition + cameraForward * 1f;

            // Set the object's position to be in the center of the screen
            obj_carried.transform.position = centerPosition;
            Debug.Log("object position: " + screenPosition.x + (screenPosition.y - 25) + 0f);

        }
    }

    void Pickup()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        layerMask = ~LayerMask.GetMask("Ignore Raycast");
        Physics.Raycast(ray, out hit, 10, layerMask);
        obj_Looking = hit.collider.GameObject();

            if (scr_player_movement.time >= 0.02) 
            { obj_last_looked = hit.collider.GameObject(); 
                Debug.Log(""+ obj_last_looked.name); 
            }


            if (obj_Looking != null)
            {
                scr_pickupable2 = obj_Looking.GetComponent<scr_pickupable>();
                obj_Looking.layer = 3;
                scr_pickupable2.ChildLayer.layer = 3;
                scr_pickupable2.UItext.text = "" + obj_last_looked.name;
                obj_Looking = obj_last_looked;
                obj_Looking = null;
            }
            else
            {
            scr_pickupable3 = obj_last_looked.GetComponent<scr_pickupable>();
            obj_last_looked.layer = 0;
            scr_pickupable3.ChildLayer.layer = 0;
            scr_pickupable3.UItext.text = "";
            }

        if (Input.GetMouseButtonDown(1)) 
        {
            if (state == 1)
            {
                layerMask = ~LayerMask.GetMask("Ignore Raycast");
                if (Physics.Raycast(ray, out hit, 10, layerMask))
                {
                    obj_carried = hit.collider.GameObject();
                    if (obj_carried != null)
                    {
                        is_active = true;
                        rb = obj_carried.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            //store original matterial in Mat change material to transparent
                            ParentMesh = obj_carried.GetComponent<MeshRenderer>();
                            if (ParentMesh != null)
                            {
                                ParentMat = ParentMesh.materials[0];
                                ParentMesh.material = transparentMaterial;
                            }
                            else
                            {
                                Child = obj_carried.transform.GetChild(0);
                                ChildMesh = Child.GetComponent<MeshRenderer>();
                                Mat = ChildMesh.materials[0];
                                ChildMesh.materials[0] = transparentMaterial;
                            }
                            //Refer to picked up obj's scr_pickupable
                            scr_pickupable = obj_carried.GetComponent<scr_pickupable>();
                            if (scr_pickupable != null)
                            {
                                if (scr_pickupable.audioSource != null)
                                {
                                    //play audio from scr_pickupable
                                    scr_pickupable.audioSource.Play();
                                    if (scr_pickupable.audioSource.isPlaying) { Subtitle.text = scr_pickupable.audioTranscript; }
                                }
                            }
                            rb.useGravity = false;
                        }
                        colliders = obj_carried.GetComponentsInChildren<Collider>();
                        foreach (Collider collider in colliders)
                        {
                            collider.enabled = false;
                        }
                        state = 2;
                        DisableNextFrame(obj_carried);
                    }
                }
            }
            else if (state == 2)
            {
                rb = obj_carried.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                }
                colliders = obj_carried.GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    collider.enabled = true;
                }
                obj_carried = null;
                is_active = false;
                //give back the original material
                if (ParentMesh != null) { ParentMesh.material = ParentMat; }
                else { ChildMesh.material = Mat; }
                state = 1;
            }
        }
    }

    IEnumerator DisableNextFrame(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        obj.SetActive(false);
    }

    /*oid CheckDrop()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        isCarrying = false;
        carriedObject = null;
    }*/
}