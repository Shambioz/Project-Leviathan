using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_score_shower : MonoBehaviour
{
    public TMP_Text txt_artifacts_saved;
    public TMP_Text txt_thiefs_paralyzed;
    public TMP_Text txt_points_excl;
    public TMP_Text txt_points_today;
    public TMP_Text txt_family_multiplier;
    public TMP_Text txt_points_total;
    public TMP_Text txt_cash_total;

    public static int total_points = 0;
    public static int total_cash = 0;
    public static int family_modifier = 15;

    // Start is called before the first frame update
    void Awake()
    {

        txt_artifacts_saved.text = scr_fixing_after_theo_fucked_up_again.artifacts_saved.ToString();
        txt_thiefs_paralyzed.text = scr_fixing_after_theo_fucked_up_again.thiefs_paralyzed.ToString();
        txt_points_excl.text = total_points.ToString();
        txt_points_today.text = scr_fixing_after_theo_fucked_up_again.points_day.ToString();
        txt_family_multiplier.text = family_modifier.ToString();
        //txt_artifacts_saved.text = scr_fixing_after_theo_fucked_up_again.artifacts_saved.ToString();

        total_points += scr_fixing_after_theo_fucked_up_again.points_day * family_modifier;
        total_cash += scr_fixing_after_theo_fucked_up_again.cash;

        txt_points_total.text = total_points.ToString();
        txt_cash_total.text = total_cash.ToString();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
