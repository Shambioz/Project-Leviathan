using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void EasyMode()
    {
        SceneManager.LoadScene("Game");
    }

    public void NormalMode()
    {
        SceneManager.LoadScene("Game");
    }

    public void HardMode()
    {
        SceneManager.LoadScene("Game");
    }
}
