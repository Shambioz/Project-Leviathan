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
    public GameObject the_one_you_picked_up;
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
            Debug.Log("object position: " + screenPosition.x + (screenPosition.y-25) + 0f);
        }
    }

    void Pickup()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (state == 1)
            {
                layerMask = ~LayerMask.GetMask("Ignore Raycast");
                if (Physics.Raycast(ray, out hit, 10, layerMask))
                {
                    obj_carried = hit.collider.GameObject();
                    the_one_you_picked_up = hit.collider.GameObject();
                    if (obj_carried != null)
                    {
                        is_active = true;
                        is_carrying = true;
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
                        DisableNextFrame(obj_carried);
                    }
                }
            }
            else if (state == 2)
            {
                is_carrying = false;
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