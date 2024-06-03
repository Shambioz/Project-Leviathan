using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_fixing_after_theo_fucked_up_again : MonoBehaviour
{
    public TMP_Text txt_score;

    public int artems_points = 0;
    public bool overrrino = false;


    public static int points_excl = 0;
    public static int points_day = 0;
    public static int points_thief = 0;
    public static int cash = 0;

    public static int artifacts_saved = 0;
    public static int thiefs_paralyzed = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (overrrino == true)
        {
            SceneManager.LoadScene("ScoreScreen");
        }
        else if (artems_points != 0)
        {
            if (artems_points == -30)
            {
                overrrino = true;
                artems_points = 0;
            }
            else if (artems_points == 5)
            {
                points_day += artems_points;
                artifacts_saved++;
                cash += 30;
                artems_points = 0;
            }
            else if (artems_points == 2)
            {
                points_day += artems_points;
                cash += 10;
                artems_points = 0;
            }
            else if (artems_points == 1)
            {
                points_day += artems_points;
                thiefs_paralyzed++;
                cash += 5;
                artems_points = 0;
            }
            else if (artems_points == -2)
            {
                points_day += artems_points;
                cash -= 10;
                artems_points = 0;
            }
        }
        txt_score.text = "Today's Score: " + points_day.ToString();
    }
}
