using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_scene_loader : MonoBehaviour
{
    // Start is called before the first frame update

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadGameScene()
    {
        if (scr_newspaper_menager.game_lost == true)
        {
            SceneManager.LoadScene(6);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
