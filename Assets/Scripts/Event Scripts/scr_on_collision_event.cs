using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class scr_on_collision_event : MonoBehaviour
{
    public GameObject EffectedObject;
    public GameObject LookAt;
    public TextMeshProUGUI Subtitle;
    public float Switch = 0f;
    private AudioSource AudioSource;
    private float AudioClipLength;
    public float PredictedEndTime = 0;
    public string[] Text;
    public PlayerBehavior PlayerBehavior;
    public scr_player_movement scr_player_movement;
    public AudioSource[] Audio;
    public scr_audio_manager scr_audio_manager;
    public float currenttime = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Audio.Length; i++)
        {
            if (Audio[i] != null)
            {
                AudioClipLength = AudioClipLength + Audio[i].clip.length;
            }

        }
    }

    void Update()
    {
        currenttime = Time.time;
        if (PredictedEndTime != 0 && Time.time >= PredictedEndTime) 
        {
            //Toffle player movement back on
            scr_player_movement.CanMove = 1;
            //empty camera target to resume normal functionality
            PlayerBehavior.target = null;
            //empty camera target to resume normal functionality
            PredictedEndTime = 0;
            //destroy this component when subtitle has been activated at end
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.name == EffectedObject.name)
            {
                Debug.Log("Entered");
            
            //Toggle player ability to move off
                scr_player_movement.CanMove = 0;
            //Calculate time for audio clip will be done
                PredictedEndTime = Time.time + AudioClipLength;
            //define Transform target in scr_camera movement
            PlayerBehavior.target = LookAt.GetComponent<Transform>();
            //if (AudioSource.isPlaying){ Subtitle.text = "" + Text;}
            scr_audio_manager.audioSources = Audio;
                scr_audio_manager.displayTexts = Text;
                scr_audio_manager.PlayAudioAndDisplayText();
            }
    }


   /* private IEnumerator WaitForSpeech()
    {

        for (int i = 0; i < Audio.Length; i++)
        {
            if (Audio[i] != null)
            {
                //define Transform target in scr_camera movement
                PlayerBehavior.target = LookAt.GetComponent<Transform>();
                // Wait for the duration of the audio clip
                yield return new WaitForSeconds(audioSources[i].clip.length);
            }

        }
        tmpText.text = null;
    }*/
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
