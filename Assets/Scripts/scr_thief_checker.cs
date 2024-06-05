using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_thief_checker : MonoBehaviour
{
    public float checkInterval = 1f;
    public float timeWithoutThievesThreshold = 15f;
    [SerializeField] float timeWithoutThieves = 0f;
    public scr_fixing_after_theo_fucked_up_again points;

    void Start()
    {
        StartCoroutine(CheckForThieves());
    }

    IEnumerator CheckForThieves()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            scr_thief_behaviour[] thieves = FindObjectsOfType<scr_thief_behaviour>();
            if (thieves.Length == 0)
            {
                timeWithoutThieves += checkInterval;
                if (timeWithoutThieves >= timeWithoutThievesThreshold)
                {
                    NoThievesForThresholdTime();
                }
            }
            else
            {
                timeWithoutThieves = 0f;
            }
        }
    }
    void NoThievesForThresholdTime()
    {
        points.artems_points += 2;
        timeWithoutThieves = 0;
    }
}
