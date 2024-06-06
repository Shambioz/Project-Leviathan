using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_scene_loader : MonoBehaviour
{
    // Start is called before the first frame update

    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex == 2)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadGameScene()
    {
        if (scr_newspaper_menager.game_lost == true)
        {
            SceneManager.LoadScene(6);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene(2);
        }
    }
}
