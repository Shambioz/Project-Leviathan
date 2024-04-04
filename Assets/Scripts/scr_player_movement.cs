using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class scr_player_movement : MonoBehaviour
{


    public scr_pick_up_object scr_pick_up_object;
    public TextMeshProUGUI Subtitle;
    public GameObject Player;
    private float lmb;
    // Start is called before the first frame update
    void Start()
    {
        scr_pick_up_object = Player.gameObject.GetComponent<scr_pick_up_object>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            
            lmb = 2.5f;
        }
        else if (!Input.GetMouseButton(0))
        {

            lmb = 0f;
        }


        // 5
        this.transform.Translate(Vector3.forward * lmb  * Time.deltaTime);

        //delete subtitles when audio finishes
        if (!scr_pick_up_object.scr_pickupable.audioSource.isPlaying) { if (Subtitle.text != "") { Subtitle.text = ""; } }
    }
}
