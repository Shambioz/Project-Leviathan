using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class scr_pick_up_object : MonoBehaviour
{
    public bool is_carrying = false;
    public scr_inventory inventory; // Assign this in the inspector
    public Transform player; // Reference to the player's transform
    public bool is_active = false;
    private int state = 1;
    public GameObject obj_carried;
    private GameObject obj_interacting;
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
    public scr_pickupable scr_pickupable;
    //Add subtitles
    public TextMeshProUGUI Subtitle;
    public GameObject the_one_you_picked_up;
    public scr_thief_hit thief_hit;
    private bool funagain = false;
    public bool hilfe1 = false;
    void Update()
    {

        Pickup();
    }

    void LateUpdate()
    {
        if (funagain == true && hilfe1 == false)
        {
            obj_carried = null;
        }
        else if (funagain == true)
        {
            is_active = true;
            is_carrying = true;
            funagain = false;
            hilfe1 = false;
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
                    ChildMesh.material = transparentMaterial;
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
            Debug.Log("new State: " + state);

            DisableNextFrame(obj_carried);
        }

        // Get the player's position in world space
        playerPosition = scr_player_manager.instance.GetPlayerPosition();
        //Debug.Log("player position: " + player);
        //onvert the player's position to screen coordinates
        cameraPosition = Camera.main.transform.position;
        cameraForward = Camera.main.transform.forward;

        centerPosition = cameraPosition + cameraForward * 1f;

        //Set the object's position to be in the center of the screen
        obj_carried.transform.position = centerPosition;
        //Debug.Log("object position: " + screenPosition.x + (screenPosition.y-25) + 0f);
    }

    void Pickup()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            Debug.Log("State: " + state);
            if (state == 1)
            {
                layerMask = ~LayerMask.GetMask("Ignore Raycast");
                if (Physics.Raycast(ray, out hit, 10, layerMask))
                {
                    obj_carried = hit.collider.GameObject();
                    the_one_you_picked_up = hit.collider.GameObject();
                    thief_hit = hit.collider.GameObject().GetComponent<scr_thief_hit>();
                    if (thief_hit != null)
                    {
                        thief_hit.help1 = true;
                        funagain = true;
                    }
                    else
                    {
                        if (obj_carried != null && obj_carried.GetComponent<scr_pickupable>() != null && obj_carried.GetComponent<scr_pickupable>().picked == false)
                        {
                            is_active = true;
                            is_carrying = true;
                            obj_carried.GetComponent<scr_pickupable>().picked = true;
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
                                    ChildMesh.material = transparentMaterial;
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
                            Debug.Log("new State: " + state);

                            DisableNextFrame(obj_carried);
                        }
                    }
                }
            }
            else if (state == 2)
            {
                Debug.Log("working? state: " + state);

                is_carrying = false;
                obj_carried.GetComponent<scr_pickupable>().picked = false;
                rb = obj_carried.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.isKinematic = false;
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
                DisableNextFrame(obj_carried);
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