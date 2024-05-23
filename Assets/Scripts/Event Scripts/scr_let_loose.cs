using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_let_loose : MonoBehaviour
{
    public GameObject EffectedObject;
    public GameObject LookAt;
    public TextMeshProUGUI Subtitle;
    public float Switch = 0f;
    private AudioSource AudioSource;
    private float AudioClipLength;
    public float PredictedEndTime = 0;
    public AudioSource Audio1;
    public string Text;
    public PlayerBehavior PlayerBehavior;
    public scr_player_movement scr_player_movement;
    public scr_kick_thief_event scr_kick_thief_event;
    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = Audio1;
        AudioClipLength = AudioSource.clip.length;
    }

    void Update()
    {
        if (PredictedEndTime != 0 && Time.time >= PredictedEndTime)
        {
            //Erase subtitle
            Subtitle.text = "";
            //Toffle player movement back on
            scr_player_movement.CanMove = 1;
            //empty camera target to resume normal functionality
            PlayerBehavior.target = null;
            //empty camera target to resume normal functionality
            PredictedEndTime = 0;
            //destroy this component when subtitle has been activated at end
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == EffectedObject.name)
        {
            Debug.Log("Entered");
            //play specified Audio
            AudioSource.Play();
            //tell audio
            scr_player_movement.Audio = AudioSource;
            //define Transform target in scr_camera movement
            PlayerBehavior.target = LookAt.GetComponent<Transform>();
            //Toggle player ability to move off
            scr_player_movement.CanMove = 0;
            //Calculate time for audio clip will be done
            PredictedEndTime = Time.time + AudioClipLength;
            //if audio is playing change text in TextMeshProUGUI for subtitle & trigger destruction
            if (AudioSource.isPlaying) { Subtitle.text = "" + Text; }
            //destroy thief
            Destroy(other.gameObject);
            scr_kick_thief_event.button = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if player is no longer inside collider stop audio & text
        if (other.gameObject.name == EffectedObject.name)
        {
            AudioSource.Stop();
            Subtitle.text = "";
        }
    }


}
