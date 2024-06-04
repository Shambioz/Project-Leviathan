using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scr_newspaper_menager : MonoBehaviour
{
    public TMP_Text date;

    private List<int> list = new List<int> {1, 2, 3, 4, 5};
    private List<int> list_backup = new List<int> {1, 2, 3, 4, 5};

    public Image newspaper;
    public Sprite day2;
    public Sprite end_victoria;
    public Sprite end_fail;
    public Sprite news1;
    public Sprite news2;
    public Sprite news3;
    public Sprite news4;
    public Sprite news5;

    public static bool game_lost = false;

    // Start is called before the first frame update
    void Awake()
    {
        //scr_day_cycle.DayCount = 2;
        //scr_day_cycle.DayCount = 78;

        if (scr_day_cycle.DayCount == 2 && game_lost != true)
        {
            newspaper.sprite = day2;
        }
        else if (scr_day_cycle.DayCount > 2 && game_lost != true)
        {
            if (list.Count == 0)
            {
                list = list_backup;
            }
            int rannumber = Random.Range(0, list.Count);
            int thenumber = list[rannumber];
            list.RemoveAt(rannumber);
            switch (thenumber)
            {
                default:
                    break;
                case 1:
                    newspaper.sprite = news1;
                    break;
                case 2:
                    newspaper.sprite = news2;
                    break;
                case 3:
                    newspaper.sprite = news3;
                    break;
                case 4:
                    newspaper.sprite = news4;
                    break;
                case 5:
                    newspaper.sprite = news5;
                    break;
            }
        }
        else if (game_lost == true)
        {
            if (scr_day_cycle.DayCount < 4)
            {
                newspaper.sprite = end_fail;
            }
            else
            {
                newspaper.sprite = end_victoria;
            }
        }

        int day = 7;
        int month = 5;
        int year = 2025;

        day += scr_day_cycle.DayCount;
        while (day > 30)
        {
            day -= 30;
            month++;
        }
        while (month > 12)
        {
            month -= 12;
            year++;
        }

        string datumday;
        if (day < 10)
        {
            datumday = "0" + day.ToString() + ".";
        }
        else
        {
            datumday = day.ToString() + ".";
        }
        string datummonth;
        if (month < 10)
        {
            datummonth = "0" + month.ToString() + ".";
        }
        else
        {
            datummonth = month.ToString() + ".";
        }

        string datum = datumday + datummonth + "2025";

        date.text = datum;
    }

    void Update()
    {
        
    }
}
