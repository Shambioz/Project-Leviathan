using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_pause_menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    public GameObject UI;
    public bool GamePaused = false;
    public scr_audio_manager Audio;
    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.activeSelf || SettingsMenu.activeSelf)
        {
            GamePaused = true;
        }
        else
        {
            GamePaused = false;
        }
        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.activeSelf && !SettingsMenu.activeSelf)
        {
            Audio.PauseAllAudio();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UI.SetActive(false);
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.activeSelf)
        {
            Audio.ResumeAllAudio();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UI.SetActive(true);
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UI.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Settings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void LeaveSettings()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
