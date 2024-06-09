using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class scr_tut_triggers : MonoBehaviour
{
    [SerializeField] private int magic;
    public GameObject EffectedObject;
    public GameObject LookAt;
    public TextMeshProUGUI Subtitle;
    public float Switch = 0f;
    private AudioSource AudioSource;
    private float AudioClipLength;
    public float PredictedEndTime = 0;
    public AudioSource Audio1;
    public string[] Text;
    public PlayerBehavior PlayerBehavior;
    public scr_player_movement scr_player_movement;
    public AudioSource[] Audio;
    public scr_audio_manager scr_audio_manager;
    public float currenttime = 0;
    public scr_tut_thief_shyt hp;
    private bool works = true;
    private bool works1 = true;
    private bool works2 = true;
    public Collider BigBoy;
    public Collider Customer;
    public Collider Picked;
    public scr_tut_customer cunter;
    public scr_pickupable rock;
    public Collider Placed;
    public Collider charge;
    // Start is called before the first frame update
    void Start()
    {
        cunter = FindObjectOfType<scr_tut_customer>();
        hp = FindObjectOfType<scr_tut_thief_shyt>();
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
        if(magic == 1)
        {
            if (hp.hp <= 0 && works)
            {
                BigBoy.enabled = true;
                works = false;
            }
        }
        else if(magic == 2)
        {
            if (cunter.shot && works1)
            {
                Customer.enabled = true;
                works1 = false;
            }
        }
        else if(magic == 3)
        {
            if (rock.picked && works2)
            {
                Debug.Log("Blyat");
                charge.enabled = true;
                Picked.enabled = true;
                works2 = false;
            }
        }
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
            //define Transform target in scr_camera movement
            PlayerBehavior.target = LookAt.GetComponent<Transform>();
            //if (AudioSource.isPlaying){ Subtitle.text = "" + Text;}
            scr_audio_manager.audioSources = Audio;
            scr_audio_manager.displayTexts = Text;
            PredictedEndTime = Time.time + AudioClipLength;
            scr_audio_manager.PlayAudioAndDisplayText();
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

    public void OnFunction()
    {
        if (hp.hp <= 0)
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
            works = false;
            scr_audio_manager.PlayAudioAndDisplayText();
            currenttime = 0;
        }
    }


    public void Customer1()
    {
        if (cunter.shot)
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
            works = false;
            scr_audio_manager.PlayAudioAndDisplayText();
        }
    }
}
