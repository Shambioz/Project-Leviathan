using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class scr_player_movement : MonoBehaviour
{


    public scr_pick_up_object scr_pick_up_object;
    public TextMeshProUGUI Subtitle;
    public GameObject Player;
    private float movespd = 3f;
    private float flyspd = 2.5f;
    private float rotspeed = 200f;
    // Start is called before the first frame update
    void Start()
    {

        scr_pick_up_object = Player.gameObject.GetComponent<scr_pick_up_object>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movespd);
        }
        //Move Back
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-1 * Vector3.forward * Time.deltaTime * movespd);
        }
        // Move left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movespd);
        }
        // Move right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movespd);
        }

        // Fly up
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * Time.deltaTime * flyspd);
        }
        // Fly down
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.Translate(Vector3.down * Time.deltaTime * flyspd);
        }



        // 5
        //this.transform.Translate(Vector3.forward * lmb  * Time.deltaTime);



        //delete subtitles when audio finishes
        if (!scr_pick_up_object.scr_pickupable.audioSource.isPlaying) { if (Subtitle.text != "") { Subtitle.text = ""; } }
    }


}