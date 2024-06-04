using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class scr_money_menagement : MonoBehaviour
{
    public TMP_Text savings;
    public TMP_Text b_food;
    public TMP_Text f_food;
    public TMP_Text b_drinks;
    public TMP_Text f_drinks;
    public TMP_Text med;
    public TMP_Text b_ent;
    public TMP_Text f_ent;
    public TMP_Text heating;
    public TMP_Text total_cash;



    public TMP_Text wife;
    public TMP_Text son;
    public TMP_Text mother;
    public TMP_Text mother_in_law;
    public TMP_Text brother;


    public Toggle tog1;
    public Toggle tog2;
    public Toggle tog3;
    public Toggle tog4;
    public Toggle tog5;
    public Toggle tog6;
    public Toggle tog7;
    public Toggle tog8;


    private string[] stringen = new string[5];
    //private int[] dying = new int[5];


    public static int[,] family = new int[5 , 7]; //wife, son, mother, mother-in-law, brother  //alive, healthy, saturated, warm, entertained, happy, hydrared
    public static int rent_cost = 30;
    public static int common_food_cost = 1;
    public static int fancy_food_cost = 3;
    public static int common_drinks_cost = 1;
    public static int fancy_drinks_cost = 2;
    public static int common_entertaintment_cost = 2;
    public static int fancy_entertaintment_cost = 4;
    public static int medications_cost = 2;
    public static int heating_cost = 20;
    private bool[] selected = new bool[9];
    //private int[] costs = new int[9];
    private int total_cost = 0;
    private static int family_members_alive = 0;
    private int family_members_sick = 1;
    private static bool first_execusion = false;
    private bool last_execusion = false;
    private int curr_cash = 0;
    private bool healthy = true;
    private int family_modifier = 3;
    private bool help = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if (first_execusion == false)
        {
            first_execusion = true;
            for (int i = 0; i < 5; i++)
            {
                family[i, 0] = 100;
                family[i, 1] = 100;
                family[i, 2] = 100;
                family[i, 3] = 100;
                family[i, 4] = 100;
                family[i, 5] = 100;
                family[i, 6] = 100;
            }
            family_members_alive = 5;
        }
        curr_cash = scr_score_shower.total_cash;
        family_members_sick = 1;
        selected[0] = true;
        for (int i = 1; i < 9; i++)
        {
            selected[i] = false;
        }
        for (int i = 0; i < 5; i++)
        {
            if (family[i, 1] < 40)
            {
                family_members_sick++;
            }
            if (family[i, 1] < 20)
            {
                family_members_sick++;
            }
        }
        total_cost = 30;
        savings.text = scr_score_shower.total_cash.ToString();
        b_food.text = (common_food_cost * family_members_alive).ToString();
        f_food.text = (fancy_food_cost * family_members_alive).ToString();
        b_drinks.text = (common_drinks_cost * family_members_alive).ToString();
        f_drinks.text = (fancy_drinks_cost * family_members_alive).ToString();
        med.text = (medications_cost * family_members_sick).ToString();
        b_ent.text = (common_entertaintment_cost * family_members_alive).ToString();
        f_ent.text = (fancy_entertaintment_cost * family_members_alive).ToString();
        heating.text = heating_cost.ToString();
        scr_score_shower.family_modifier = 0;

        for (int i = 0; i < 5; i++)
        {
            stringen[i] = "";
            family_modifier = 3;
            help = false;
            //dying[i] = 0;
            healthy = true;
            if (family[i, 0] == -17)
            {
                healthy = false;
                stringen[i] = "Dead";
                family_modifier = 1;    
            }
            else
            {
                if (family[i, 1] <= 40)
                {
                    healthy = false;
                    stringen[i] += "Sick \n";
                    family_modifier--;
                    help = true;
                }
                if (family[i, 2] <= 40)
                {
                    healthy = false;
                    stringen[i] += "Hungry \n";
                    if (help == true)
                    {
                        help = false;
                    }
                    else if (family_modifier != 1)
                    {
                        family_modifier--;
                        help = true;
                    }
                }
                if (family[i, 3] <= 40)
                {
                    healthy = false;
                    stringen[i] += "Cold \n";
                    if (help == true)
                    {
                        help = false;
                    }
                    else if (family_modifier != 1)
                    {
                        family_modifier--;
                        help = true;
                    }
                }
                if (family[i, 4] <= 40)
                {
                    healthy = false;
                    stringen[i] += "Bored \n";
                    if (help == true)
                    {
                        help = false;
                    }
                    else if (family_modifier != 1)
                    {
                        family_modifier--;
                        help = true;
                    }
                }
                if (family[i, 5] <= 40)
                {
                    healthy = false;
                    stringen[i] += "Unhappy \n";
                    if (help == true)
                    {
                        help = false;
                    }
                    else if (family_modifier != 1)
                    {
                        family_modifier--;
                        help = true;
                    }
                }
                if (family[i, 6] <= 40)
                {
                    healthy = false;
                    stringen[i] += "Dehydrated \n";
                    if (help == true)
                    {
                        help = false;
                    }
                    else if (family_modifier != 1)
                    {
                        family_modifier--;
                        help = true;
                    }
                }
            }
            if (healthy == true)
            {
                stringen[i] = " . . \n|__|";
            }
            scr_score_shower.family_modifier += family_modifier;
        }
        wife.text = stringen[0];
        son.text = stringen[1];
        mother.text = stringen[2];
        mother_in_law.text = stringen[3];
        brother.text = stringen[4];
    }

    // Update is called once per frame
    void Update()
    {
        total_cash.text = (scr_score_shower.total_cash - total_cost).ToString();
        if (last_execusion == true)
        {
            last_execusion = false;
            if (selected[1] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 2] += 60;
                }
            }
            if (selected[2] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 2] += 10;
                    family[i, 5] += 10;
                }
            }
            if (selected[3] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 6] += 80;
                    //family[i, 5] += 10;
                }
            }
            if (selected[4] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 6] += 20;
                    family[i, 5] += 10;
                }
            }
            if (selected[5] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 4] += 50;
                    family[i, 5] += 10;
                }
            }
            if (selected[6] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 4] += 10;
                    family[i, 5] += 10;
                    family[i, 2] += 10;
                    family[i, 6] += 10;
                }
            }
            if (selected[7] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 1] += 30;
                }
            }
            if (selected[8] == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    family[i, 3] += 50;
                }
            }

            family[2, 1] -= 10;
            family[3, 1] -= 10;

            family[0, 2] -= 50;
            family[1, 2] -= 60;
            family[2, 2] -= 40;
            family[3, 2] -= 40;
            family[4, 2] -= 50;

            family[0, 3] -= 30;
            family[1, 3] -= 40;
            family[2, 3] -= 40;
            family[3, 3] -= 40;
            family[4, 3] -= 30;

            family[0, 4] -= 30;
            family[1, 4] -= 50;
            family[2, 4] -= 40;
            family[3, 4] -= 40;
            family[4, 4] -= 30;

            family[0, 5] -= 20;
            family[1, 5] -= 20;
            family[2, 5] -= 20;
            family[3, 5] -= 20;
            family[4, 5] -= 20;

            family[0, 6] -= 60;
            family[1, 6] -= 70;
            family[2, 6] -= 70;
            family[3, 6] -= 70;
            family[4, 6] -= 60;


            for (int i = 0; i < 5; i++)
            {
                //family[i, 1] += 30;
                if (family[i, 0] != -17)
                {
                    family[i, 0] += 20;
                }
                for (int j = 2; j < 7; j++)
                {
                    if (family[i, j] > 100)
                    {
                        family[i, j] = 100;
                    }
                    else if (family[i, j] < 0)
                    {
                        family[i, j] = 0;
                    }
                    else if (family[i, j] <= 60 && family[i, j] > 40)
                    {
                        family[i, 1] -= 10;
                        if (family[i, 1] < 0)
                        {
                            family[i, 1] = 0;
                        }
                    }
                    else if (family[i, j] <= 40)
                    {
                        family[i, 1] -= 20;
                        family[i, 0] -= 10;
                        if (family[i, 1] < 0)
                        {
                            family[i, 1] = 0;
                        }
                        if (family[i, 0] < 0)
                        {
                            family[i, 0] = 0;
                        }
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (family[i, 1] <= 40)
                {
                    family[i, 0] -= 40;
                    if (family[i, 0] < 0 && family[i, 0] != -17)
                    {
                        family[i, 0] = 0;
                    }
                }
                if (family[i, 0] == 0)
                {
                    family[i, 0] = -17;
                    family_members_alive--;
                }
            }

            scr_score_shower.total_cash -= total_cost;
        }
    }

    public void Selected(int selindex)
    {
        if (selected[selindex] == false)
        {
            if (selindex == 1)
            {
                total_cost += common_food_cost * family_members_alive;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= common_food_cost * family_members_alive;
                    tog1.isOn = false;
                }
            }
            else if (selindex == 2)
            {
                total_cost += fancy_food_cost * family_members_alive;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= fancy_food_cost * family_members_alive;
                    tog2.isOn = false;
                }
            }
            else if (selindex == 3)
            {
                total_cost += common_drinks_cost * family_members_alive;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= common_drinks_cost * family_members_alive;
                    tog3.isOn = false;
                }
            }
            else if (selindex == 4)
            {
                total_cost += fancy_drinks_cost * family_members_alive;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= fancy_drinks_cost * family_members_alive;
                    tog4.isOn = false;
                }
            }
            else if (selindex == 6)
            {
                total_cost += common_entertaintment_cost * family_members_alive;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= common_entertaintment_cost * family_members_alive;
                    tog6.isOn = false;
                }
            }
            else if (selindex == 7)
            {
                total_cost += fancy_entertaintment_cost * family_members_alive;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= fancy_entertaintment_cost * family_members_alive;
                    tog7.isOn = false;
                }
            }
            else if (selindex == 5)
            {
                total_cost += medications_cost * family_members_sick;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= medications_cost * family_members_sick;
                    tog5.isOn = false;
                }
            }
            else if (selindex == 8)
            {
                total_cost += 20;
                if (scr_score_shower.total_cash - total_cost > 0)
                {
                    selected[selindex] = true;
                }
                else
                {
                    total_cost -= 20;
                    tog8.isOn = false;
                }
            }
        }
        else
        {
            if (selindex == 1)
            {
                total_cost -= common_food_cost * family_members_alive;
                selected[selindex] = false;
            }
            else if (selindex == 2)
            {
                total_cost -= fancy_food_cost * family_members_alive;
                selected[selindex] = false;
            }
            else if (selindex == 3)
            {
                total_cost -= common_drinks_cost * family_members_alive;
                selected[selindex] = false;
            }
            else if (selindex == 4)
            {
                total_cost -= fancy_drinks_cost * family_members_alive;
                selected[selindex] = false;
            }
            else if (selindex == 6)
            {
                total_cost -= common_entertaintment_cost * family_members_alive;
                selected[selindex] = false;
            }
            else if (selindex == 7)
            {
                total_cost -= fancy_entertaintment_cost * family_members_alive;
                selected[selindex] = false;
            }
            else if (selindex == 5)
            {
                total_cost -= medications_cost * family_members_sick;
                selected[selindex] = false;
            }
            else if (selindex == 8)
            {
                total_cost -= 20;
                selected[selindex] = false;
            }
        }
    }
}
