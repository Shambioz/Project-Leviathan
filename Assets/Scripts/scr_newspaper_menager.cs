using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_newspaper_menager : MonoBehaviour
{
    public TMP_Text date;
    //public TMP_Text button_text;

    public Sprite go_to_work;
    public Sprite cont;

    public Button button;

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

    public AudioClip[] clippen = new AudioClip[5];
    public AudioSource[] surcen = new AudioSource[5];

    public static bool game_lost = false;

    // Start is called before the first frame update
    void Awake()
    {
        //scr_day_cycle.DayCount = 2;
        //scr_day_cycle.DayCount = 78;

        //ExecuteTheMusic();

        //game_lost = true;

        Cursor.lockState = CursorLockMode.None;
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
                ExecuteTheMusic();
            }
            else
            {
                newspaper.sprite = end_victoria;
                ExecuteTheMusic();
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
        if (game_lost == true)
        {
            button.image.sprite = cont;
        }
        else
        {
            button.image.sprite = go_to_work;
        }

        scr_fixing_after_theo_fucked_up_again.artifacts_saved = 0;
        scr_fixing_after_theo_fucked_up_again.thiefs_paralyzed = 0;
        //scr_score_shower.total_points = 0;
        scr_fixing_after_theo_fucked_up_again.points_day = 0;
    }

    public void ExecuteTheMusic()
    {
        int rannumber = Random.Range(0, 101);
        //rannumber = 74;
        if (rannumber == 74)
        {
            surcen[2].loop = true;
            surcen[2].Play();
        }
        else if (rannumber == 31)
        {
            surcen[3].loop = true;
            surcen[3].Play();
        }
        else if (rannumber == 69)
        {
            surcen[4].loop = true;
            surcen[4].Play();
        }
        else if (scr_day_cycle.DayCount < 4)
        {
            surcen[1].loop = true;
            surcen[1].Play();
        }
        else
        {
            surcen[0].loop = true;
            surcen[0].Play();
        }
    }
}
