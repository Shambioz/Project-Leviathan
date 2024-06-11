using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_scene_loader : MonoBehaviour
{
    // Start is called before the first frame update

    public static void LoadScene(int sceneIndex)
    {
        if (scr_language.english == false)
        {
            sceneIndex += 6;
        }
        if (sceneIndex == 2 || sceneIndex == 8)
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
        if (scr_language.english == true)
        {
            if (scr_newspaper_menager.game_lost == true)
            {
                SceneManager.LoadScene(6);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                scr_newspaper_menager.game_lost = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            if (scr_newspaper_menager.game_lost == true)
            {
                SceneManager.LoadScene(12);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                scr_newspaper_menager.game_lost = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SceneManager.LoadScene(8);
            }
        }
    }

    public void TheosFunnyVariable(bool variable)
    {
        scr_money_menagement.theos_variable = variable;
    }

    public void Tutorialornot()
    {
        if (scr_language.english == true)
        {
            if (scr_money_menagement.theos_variable == true)
            {
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                SceneManager.LoadScene(2);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            if (scr_money_menagement.theos_variable == true)
            {
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                SceneManager.LoadScene(8);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

}
