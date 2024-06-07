using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scr_settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Volume(float volume)
    {
        audioMixer.SetFloat("ExposedVolume", volume);
    }
}
