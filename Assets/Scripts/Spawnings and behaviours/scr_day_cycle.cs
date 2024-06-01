using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_day_cycle : MonoBehaviour
{
    public Material Day;
    public Material Night;
    public float timer;
    public int DayCount = 1;
    public int DayTime = 180;
    public GameObject EndUI;
    public int leave = 0;
    public scr_customers_navigation navigation;
    public TextMeshProUGUI Days;
    public GameObject Slider;
    private bool ended = false;
    // Start is called before the first frame update
    void Start()
    {
        navigation = FindObjectOfType<scr_customers_navigation>();
        EndUI.SetActive(false);
        Day = RenderSettings.skybox;
        timer = 0f;
        RenderSettings.skybox = Day;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EndUI.activeSelf)
        {
            timer += Time.deltaTime;
        }

        if(timer > 15)
        {
            Debug.Log("wooork");
            EndDay();
        }
        
        if(ended && navigation.count == 0)
        {
            ShowUI();
            ended = false;
        }
    }

    void EndDay()
    {
        if (navigation != null)
        {
            navigation.CanSpawn = false;
        }
        leave = 1;
        Debug.Log(leave + " hmm");
        timer = 0f;
        ended = true;
        scr_thief_behaviour[] thieves = FindObjectsOfType<scr_thief_behaviour>();
        foreach (scr_thief_behaviour thief in thieves)
        {
            thief.TriggerExit();
        }
        scr_customers_behaviour[] customers = FindObjectsOfType<scr_customers_behaviour>();
        foreach (scr_customers_behaviour customer in customers)
        {
            customer.TriggerExit();
        }
    }

    void ShowUI()
    {
        EndUI.SetActive(true);
        Slider.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Days.text = "Congratulations! You have completed day: " + DayCount;
    }

    public void CanSpawn()
    {
        DayCount++;
        Slider.SetActive(true);
        navigation.CanSpawn = true;
    }

    public void Check()
    {
        Debug.Log(navigation.CanSpawn);
    }
}
