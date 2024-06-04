using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_timerr : MonoBehaviour
{
    public TextMeshProUGUI clockText;

    private float elapsedTime;
    private int startHour = 12;
    private int endHour = 18;
    private int currentHour;
    private int currentMinute;

    void Start()
    {
        elapsedTime = 0f;
        currentHour = startHour;
        currentMinute = 0;
        UpdateClockText();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int totalMinutesPassed = Mathf.FloorToInt((elapsedTime / 15f) * 30f);

        currentHour = startHour + (totalMinutesPassed / 60);
        currentMinute = totalMinutesPassed % 60;

        if (currentHour >= endHour)
        {
            currentHour = endHour;
            currentMinute = 0;
        }

        UpdateClockText();
    }

    void UpdateClockText()
    {
        string hourText = currentHour.ToString("00");
        string minuteText = currentMinute.ToString("00");
        clockText.text = $"{hourText}:{minuteText}";
    }
}
