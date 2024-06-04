using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    public AudioSource defaultBGM; // The audio source that plays initially
    public AudioSource combatBGM; // The audio source to play when the object exists
    public string objectTagToCheck = ""; // The name of the object to check for

    void Update()
    {
        // Check if the object exists in the scene
        GameObject obj = GameObject.FindWithTag(objectTagToCheck);

        if (obj != null)
        {
            // If the object exists, stop the initial audio and play the alternate audio
            if (defaultBGM.isPlaying)
            {
                defaultBGM.Stop();
                combatBGM.loop = true;
                combatBGM.Play();
            }
        }
        else
        {
            // If the object does not exist, ensure the initial audio is playing and the alternate audio is stopped
            if (!defaultBGM.isPlaying)
            {
                defaultBGM.loop = true;
                defaultBGM.Play();
                combatBGM.Stop();
            }
        }
    }
}
