using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    scr_money_menagement scr_money_menagement;
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
        scr_money_menagement.theos_variable = false;
        scr_customers_navigation.thief_chance = 0.3f;

    }

    public void NormalMode()
    {
        scr_money_menagement.theos_variable = false;
        scr_customers_navigation.thief_chance = 0.4f;

    }

    public void HardMode()
    {
        scr_money_menagement.theos_variable = false;
        scr_customers_navigation.thief_chance = 0.5f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }
}
