using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class scr_player_movement : MonoBehaviour
{


    public scr_pick_up_object scr_pick_up_object;
    public TextMeshProUGUI Subtitle;
    public AudioSource Audio;
    public GameObject Player;
    private float movespd = 10f;
    private float flyspd = 5f;
    private float rotspeed = 200f;
    public float CanMove = 1;
    public int layerMask;
    public Vector3 rayOrigin;
    public Vector3 rayDirection;
    public Rigidbody rb;

    [SerializeField] float navigationSpeed = 2.4f;
    [SerializeField] float shiftMultiplier = 2f;
    [SerializeField] float sensitivity = 1.0f;

    void Start()
    {
        
        scr_pick_up_object = Player.gameObject.GetComponent<scr_pick_up_object>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove == 1)
        {

            //Move forward
            if (Input.GetKey(KeyCode.W))
            {
                rayOrigin = transform.position;
                rayDirection = transform.forward;
                RaycastHit hit; //Ray pew pew pew
                layerMask = LayerMask.GetMask("Ignore Raycast"); //Ignore raycas is assigned to all the objects that you can't go through
                if (!Physics.Raycast(rayOrigin, rayDirection, out hit, 1, layerMask)) // Only if there isn't an object in in the place you're trying to go to, you can move
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * movespd);
                }
            }


            //Move Back
            if (Input.GetKey(KeyCode.S))
            {
                rayOrigin = transform.position;
                rayDirection = -transform.forward;
                RaycastHit hit;
                layerMask = LayerMask.GetMask("Ignore Raycast");
                if (!Physics.Raycast(rayOrigin, rayDirection, out hit, 1, layerMask))
                {
                    transform.Translate(-1 * Vector3.forward * Time.deltaTime * movespd);
                }
            }
            // Move left
            if (Input.GetKey(KeyCode.A))
            {
                rayOrigin = transform.position;
                rayDirection = -transform.right;
                RaycastHit hit;
                layerMask = LayerMask.GetMask("Ignore Raycast");
                if (!Physics.Raycast(rayOrigin, rayDirection, out hit, 1, layerMask))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * movespd);
                }
            }
            // Move right
            if (Input.GetKey(KeyCode.D))
            {
                rayOrigin = transform.position;
                rayDirection = transform.right;
                RaycastHit hit;
                layerMask = LayerMask.GetMask("Ignore Raycast");
                if (!Physics.Raycast(rayOrigin, rayDirection, out hit, 1, layerMask))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * movespd);
                }
            }

            // Fly up
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Space))
            {
                rayOrigin = transform.position;
                rayDirection = transform.up;
                RaycastHit hit;
                layerMask = LayerMask.GetMask("Ignore Raycast");
                if (!Physics.Raycast(rayOrigin, rayDirection, out hit, 1, layerMask))
                {
                    transform.Translate(Vector3.up * Time.deltaTime * flyspd);
                }
            }
            // Fly down
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.LeftShift))
            {
                rayOrigin = transform.position;
                rayDirection = -transform.up;
                RaycastHit hit;
                layerMask = LayerMask.GetMask("Ignore Raycast");
                if (!Physics.Raycast(rayOrigin, rayDirection, out hit, 1, layerMask))
                {
                    transform.Translate(Vector3.down * Time.deltaTime * flyspd);
                }
            }



        }
        // 5
        //this.transform.Translate(Vector3.forward * lmb  * Time.deltaTime);

        //delete subtitles when audio finishes
        //if (!scr_pick_up_object.scr_pickupable.audioSource.isPlaying && !Audio.isPlaying && Audio.isPlaying) { if (Subtitle.text != "") { Subtitle.text = ""; } }
    }


}