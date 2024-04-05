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
            while (other.gameObject.name == EffectedObject.name)
            {
                if (Switch == 2)
                {
                    Debug.Log("Entered");
                    AudioSource.Play();
                    if (Audio1.isPlaying)
                    {
                        Subtitle.text = "" + Text;
                    }
                }
                else
                {
                    if (Switch == 0)
                    Debug.Log("Entered");
                    AudioSource.Play();
                    if (Audio1.isPlaying)
                        {
                            Subtitle.text = "" + Text;
                        }
                    Switch = 1;
                }

                yield return new WaitForSeconds(AudioClipLength);

            }

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
