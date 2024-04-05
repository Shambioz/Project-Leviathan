using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_on_collision_event : MonoBehaviour
{
    public GameObject EffectedObject;
    public TextMeshProUGUI Subtitle;
    public float Switch = 0f;
    private AudioSource AudioSource;
    private float AudioClipLength;
    public AudioSource Audio1;
    public string Text;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = Audio1;
    }

    // Update is called once per frame
    void Update()
    {
        AudioClipLength = AudioSource.clip.length;
    }

   // void OnCollisionEnter(Collision collision)
    //{
      //  if (collision.gameObject.name == "Player")
        //{
          //  Debug.Log("Entered");
        //}
    //}

    private IEnumerator OnTriggerEnter(Collider other)
    {
            if (other.gameObject.name == EffectedObject.name)
            {
                Debug.Log("Entered");
                AudioSource.Play();
                if (AudioSource.isPlaying){ Subtitle.text = "" + Text; }
                yield return new WaitForSeconds(AudioClipLength);
                if (AudioSource.isPlaying) { Subtitle.text = ""; }
                Destroy(this);
            }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == EffectedObject.name)
        {
            AudioSource.Stop();
            Subtitle.text = "";
        }
    }


}
