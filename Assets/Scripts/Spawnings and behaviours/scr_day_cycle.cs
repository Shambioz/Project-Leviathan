using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class scr_day_cycle : MonoBehaviour
{
    public Material Day;
    public Material Night;
    public float timer;
    public static int DayCount = 1;
    public int DayTime = 180;
    public GameObject EndUI;
    public int leave = 0;
    public scr_customers_navigation navigation;
    public TextMeshProUGUI Days;
    public GameObject Slider;
    private bool ended = false;
    public GameObject LostPanel;
    public TextMeshProUGUI Lost;
    // Start is called before the first frame update
    void Start()
    {
        DayCount = 1;
        scr_thief_behaviour[] thieves = FindObjectsOfType<scr_thief_behaviour>();
        EndUI.SetActive(false);
        LostPanel.SetActive(false);
        Day = RenderSettings.skybox;
        timer = 0f;
        RenderSettings.skybox = Day;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EndUI.activeSelf || !LostPanel.activeSelf)
        {
            timer += Time.deltaTime;
        }
        
        if(EndUI.activeSelf)
        {
            timer = 0;
        }

        if (timer > 300)
        {
            Debug.Log("wooork");
            EndDay();
        }

        if (ended && navigation.count == 0)
        {
            ShowUI();
            ended = false;
        }
        
        CheckThieves();
    }

    void EndDay()
    {
        navigation.CanSpawn = false;
        leave = 1;
        Debug.Log(leave + " hmm");
        ended = true;
        scr_thief_behaviour[] thieves = FindObjectsOfType<scr_thief_behaviour>();
        foreach (scr_thief_behaviour thief in thieves)
        {
            Debug.Log("ido");
            thief.TriggerExit();
        }
        scr_customers_behaviour[] customers = FindObjectsOfType<scr_customers_behaviour>();
        foreach (scr_customers_behaviour customer in customers)
        {
            Debug.Log("ido");
            customer.TriggerExit();
        }
    }

    void ShowUI()
    {
        Debug.Log("showing");
        EndUI.SetActive(true);
        Slider.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Days.text = "Congratulations! You have completed day: " + DayCount;
    }

    public void NextDay()
    {
        DayCount++;
        Slider.SetActive(true);
        navigation.CanSpawn = true;
        leave = 0;
        navigation.StartSpawning();
        timer = 0f;
    }
    public void Restart()
    {
        navigation = FindObjectOfType<scr_customers_navigation>();
        navigation.count = 0;
        Debug.Log("Restarting");
        DayCount = 1;
        Debug.Log(DayCount + "Siri");
        navigation.CanSpawn = true;
        navigation.StartSpawning();
        LostPanel.SetActive(false);
        Slider.SetActive(true);
        
    }

    public void Check()
    {
        Debug.Log(navigation.CanSpawn);
    }
    void CheckThieves()
    {
        scr_thief_behaviour[] thieves = FindObjectsOfType<scr_thief_behaviour>();
        foreach (scr_thief_behaviour thief in thieves)
        {
            if (thief.YouLost)
            {
                /*Cursor.lockState = CursorLockMode.None;
                Slider.SetActive(false);
                LostPanel.SetActive(true);
                ended = false;
                DestroyEveryone();
                Lost.text = "You lost";*/
                Cursor.lockState = CursorLockMode.None;
                scr_newspaper_menager.game_lost = true;
                SceneManager.LoadScene(5);
                break;
            }
        }
    }
    void DestroyEveryone() 
    {
        if (navigation != null)
        {
            navigation.CanSpawn = false;
        }
        scr_thief_behaviour[] thieves = FindObjectsOfType<scr_thief_behaviour>();
        foreach (scr_thief_behaviour thief in thieves)
        {
            thief.Suicide();
        }
        scr_customers_behaviour[] customers = FindObjectsOfType<scr_customers_behaviour>();
        foreach (scr_customers_behaviour customer in customers)
        {
            customer.Suicide();
        }
    }
}
