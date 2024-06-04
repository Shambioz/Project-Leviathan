using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_dead_or_alive : MonoBehaviour
{
    public TMP_Text days;
    public TMP_Text euro;
    public TMP_Text dead;



    // Start is called before the first frame update
    void Awake()
    {
        days.text = scr_day_cycle.DayCount.ToString();
        euro.text = scr_score_shower.total_cash.ToString();
        dead.text = scr_money_menagement.family_members_alive.ToString();
    }
}
