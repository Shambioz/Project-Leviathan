using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class scr_thief_timer : MonoBehaviour
{
    public TextMeshProUGUI Timer;
    private float time;
    private float timestart;
    private float timer;

    public float totaltime = 60;


    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timestart = Time.time - time;
        timer = 60 - timestart;
        Timer.text = timer.ToString("#.0");
    }
}
