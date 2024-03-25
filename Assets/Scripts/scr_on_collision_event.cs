using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_on_collision_event : MonoBehaviour
{
    public GameObject EffectedObject;
    public TextMeshProUGUI Subtitle;
    private Collider oc;
    private AudioSource AudioSource;
    private float AudioClipLength;
    public AudioSource Audio1;

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

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("How");
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Entered");
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            while (other.gameObject.name == "Player")
            {
                Debug.Log("Entered");
                AudioSource.Play();
                yield return new WaitForSeconds(AudioClipLength);
                if (Audio1.isPlaying) { Subtitle.text = "RAAAAAAG"; }
            }

        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            AudioSource.Stop();
            Subtitle.text = "";
        }
    }


}
