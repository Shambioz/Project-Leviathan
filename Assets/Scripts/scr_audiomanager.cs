using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_audiomanager : MonoBehaviour
{
    // Array to hold the audio sources
    public AudioSource[] audioSources;

    // Array to hold the strings to be displayed
    public string[] displayTexts;

    // Reference to the TMP text component on the canvas
    public TMP_Text tmpText;

    // Function to play all audio sources and update the text
    public void PlayAudioAndDisplayText()
    {
        // Ensure the lengths of audioSources and displayTexts are the same
        if (audioSources.Length != displayTexts.Length)
        {
            Debug.LogError("AudioSources and DisplayTexts arrays must have the same length.");
            return;
        }

        // Start the coroutine to play audio sources and display texts
        StartCoroutine(PlayAudioAndDisplayTextCoroutine());
    }

    // Coroutine to play audio sources and update the text with a delay
    private IEnumerator PlayAudioAndDisplayTextCoroutine()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] != null && tmpText != null)
            {
                // Play the audio source
                audioSources[i].Play();

                // Update the TMP text
                tmpText.text = displayTexts[i];

                // Wait for the duration of the audio clip
                yield return new WaitForSeconds(audioSources[i].clip.length);
            }
            tmpText.text = null;
        }
    }
}
