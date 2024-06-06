using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_change_dif_by_button : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeByButton(float change)
    {
        scr_customers_navigation.thief_chance = change;
    }
}
