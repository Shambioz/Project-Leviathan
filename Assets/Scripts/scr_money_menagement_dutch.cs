using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class scr_money_menagement1 : MonoBehaviour
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

    public Image arrow1;
    public Image arrow2;
    public Image arrow3;
    public Image arrow4;
    public Image arrow5;
    public Image arrow6;

    public Color color;

    public AudioClip[] clips = new AudioClip[6];
    public AudioSource[] sources = new AudioSource[6];
    private bool[] played = new bool[6];


    private string[] stringen = new string[5];
    //private int[] dying = new int[5];


    public static int[,] family = new int[5, 7]; //wife, son, mother, mother-in-law, brother  //alive, healthy, saturated, warm, entertained, happy, hydrared
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
    public static int family_members_alive = 0;
    private int family_members_sick = 1;
    private static bool first_execusion = false;
    private bool last_execusion = false;
    public static int curr_cash = 0;
    private bool healthy = true;
    private int family_modifier = 3;
    private bool help = false;

    public bool tutorial_start = false;
    public bool tutorial_ongoing = false;
    public int tutorial_level = 0;

    private bool initialize = false;

    public Button button;
    public Sprite cont;
    public Sprite tutfin;



    public int tutorial_cash = 300;
    public int tutorial_rem_cash = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        color = arrow1.color;
        color.a = 0f;
        arrow1.color = color;
        color = arrow2.color;
        color.a = 0f;
        arrow2.color = color;
        color = arrow3.color;
        color.a = 0f;
        arrow3.color = color;
        color = arrow4.color;
        color.a = 0f;
        arrow4.color = color;
        color = arrow5.color;
        color.a = 0f;
        arrow5.color = color;
        color = arrow6.color;
        color.a = 0f;
        arrow6.color = color;
        Debug.Log("£A£A£A£A awake2" + tutorial_rem_cash);
        //scr_money_menagement.theos_variable = true;
        if (scr_money_menagement.theos_variable == false)
        {
            button.image.sprite = cont;
            Cursor.lockState = CursorLockMode.None;
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
            curr_cash += scr_score_shower.total_cash;
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
                    stringen[i] = "Dood";
                    family_modifier = 1;
                }
                else
                {
                    if (family[i, 1] <= 40)
                    {
                        healthy = false;
                        stringen[i] += "Ziek \n";
                        family_modifier--;
                        help = true;
                    }
                    if (family[i, 2] <= 40)
                    {
                        healthy = false;
                        stringen[i] += "Hongerig \n";
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
                        stringen[i] += "Koud \n";
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
                        stringen[i] += "Verveeld \n";
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
                        stringen[i] += "Ontevreden \n";
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
                        stringen[i] += "Uitgedroogd \n";
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
        else
        {
            button.image.sprite = tutfin;

            tutorial_cash = 330;
            savings.text = "330";
            b_food.text = "30";
            f_food.text = "50";
            b_drinks.text = "30";
            f_drinks.text = "50";
            med.text = "100";
            b_ent.text = "40";
            f_ent.text = "400";
            heating.text = "100";

            wife.text = "Ziek";
            son.text = "Ziek";
            mother.text = "Ziek";
            mother_in_law.text = "Ziek";
            brother.text = "Ziek";

            common_food_cost = 30;
            common_drinks_cost = 30;
            common_entertaintment_cost = 40;
            fancy_drinks_cost = 50;
            fancy_food_cost = 50;
            fancy_entertaintment_cost = 400;
            heating_cost = 100;
            medications_cost = 100;
            if (initialize == false)
            {
                tutorial_rem_cash = 300;
                initialize = true;
            }
            tutorial_start = true;

            for (int i = 0; i < 6; i++)
            {
                played[i] = false;
            }
        }
        Debug.Log("£A£A£A£A awake2" + tutorial_rem_cash);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("£A£A£A£A NUHDANHUSDH" + tutorial_rem_cash);
        if (scr_money_menagement.theos_variable == false)
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
        else
        {
            Debug.Log("£A£A£A TUT CASH 2: " + tutorial_rem_cash);
            Debug.Log("OKROSZKA1 " + tutorial_level);
            total_cash.text = tutorial_rem_cash.ToString();
            StartinTutorial(tutorial_level);
            if (Input.anyKeyDown && tutorial_ongoing == false && tutorial_start == true)
            {
                Debug.Log("OKROSZKA2");
                tutorial_level++;
            }
        }

    }
    public void StartinTutorial(int numberro)
    {
        if (tutorial_start == true)
        {
            Debug.Log("OKROSZKA3");
            if (played[numberro] == false)
            {
                Debug.Log("OKROSZKA4");
                sources[numberro].PlayOneShot(clips[numberro]);
                Debug.Log("OKROSZKA5");
                if (sources[numberro] != null)
                {
                    Debug.Log("OKROSZKA6");
                }
                if (sources[numberro] != null)
                {
                    Debug.Log("OKROSZKA7");
                }
                played[numberro] = true;
                tutorial_ongoing = true;
                switch (numberro)
                {
                    default:
                        break;
                    case 0:
                        //Debug.Log("I GO HERE" + arrow.transform.position);
                        break;
                    case 1:
                        color = arrow1.color;
                        color.a = 1f;
                        arrow1.color = color;
                        break;
                    case 2:
                        color = arrow1.color;
                        color.a = 0f;
                        arrow1.color = color;
                        color = arrow2.color;
                        color.a = 1f;
                        arrow2.color = color;
                        break;
                    case 3:
                        //Debug.Log("I GO HERE HAHAHA" + arrow.transform.position);
                        color = arrow2.color;
                        color.a = 0f;
                        arrow2.color = color;
                        color = arrow3.color;
                        color.a = 1f;
                        arrow3.color = color;
                        break;
                    case 4:
                        //Debug.Log("I GO HERE HAHAHA" + arrow.transform.position);
                        color = arrow3.color;
                        color.a = 0f;
                        arrow3.color = color;
                        color = arrow4.color;
                        color.a = 1f;
                        arrow4.color = color;
                        break;
                    case 5:
                        color = arrow5.color;
                        color.a = 1f;
                        arrow5.color = color;
                        break;
                }
            }
            else if (played[numberro] == true && sources[numberro].isPlaying == false)
            {
                tutorial_ongoing = false;
                if (numberro == 5)
                {
                    tutorial_start = false;
                    color = arrow5.color;
                    color.a = 1f;
                    arrow5.color = color;
                }
            }
        }
    }

    public void Selected(int selindex)
    {
        if (scr_money_menagement.theos_variable == false)
        {
            if (selected[selindex] == false)
            {
                if (selindex == 1)
                {
                    total_cost += common_food_cost * family_members_alive;
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
                    if (scr_score_shower.total_cash - total_cost >= 0)
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
        else if (scr_money_menagement.theos_variable == true)
        {
            Debug.Log("£A£A£A 0");
            if (selected[selindex] == false)
            {
                Debug.Log("£A£A£A 1");
                if (selindex == 1)
                {
                    Debug.Log("£A£A£A 2");
                    if (tutorial_rem_cash - 30 >= 0)
                    {
                        Debug.Log("£A£A£A 3");
                        tutorial_rem_cash -= 30;
                        selected[selindex] = true;
                        Debug.Log("£A£A£A EXTRA:" + tutorial_rem_cash);
                    }
                    else
                    {
                        Debug.Log("£A£A£A 4");
                        tog1.isOn = false;
                    }
                }
                else if (selindex == 2)
                {
                    if (tutorial_rem_cash - 50 >= 0)
                    {
                        tutorial_rem_cash -= 50;
                        selected[selindex] = true;
                    }
                    else
                    {
                        tog2.isOn = false;
                    }
                }
                else if (selindex == 3)
                {
                    if (tutorial_rem_cash - 30 >= 0)
                    {
                        tutorial_rem_cash -= 30;
                        selected[selindex] = true;
                    }
                    else
                    {
                        tog3.isOn = false;
                    }
                }
                else if (selindex == 4)
                {
                    if (tutorial_rem_cash - 50 >= 0)
                    {
                        tutorial_rem_cash -= 50;
                        selected[selindex] = true;
                    }
                    else
                    {
                        tog4.isOn = false;
                    }
                }
                else if (selindex == 6)
                {
                    if (tutorial_rem_cash - 40 >= 0)
                    {
                        tutorial_rem_cash -= 40;
                        selected[selindex] = true;
                    }
                    else
                    {
                        tog6.isOn = false;
                    }
                }
                else if (selindex == 7)
                {
                    if (tutorial_rem_cash - 400 >= 0)
                    {
                        tutorial_rem_cash -= 400;
                        selected[selindex] = true;
                    }
                    else
                    {
                        tog7.isOn = false;
                    }
                }
                else if (selindex == 5)
                {
                    if (tutorial_rem_cash - 100 >= 0)
                    {
                        tutorial_rem_cash -= 100;
                        selected[selindex] = true;
                    }
                    else
                    {
                        tog5.isOn = false;
                    }
                }
                else if (selindex == 8)
                {
                    if (tutorial_rem_cash - 100 >= 0)
                    {
                        tutorial_rem_cash -= 100;
                        selected[selindex] = true;
                    }
                    else
                    {
                        tog8.isOn = false;
                    }
                }
            }
            else if (selected[selindex] == true)
            {
                //Debug.Log("£A£A£A 1");
                if (selindex == 1)
                {
                    //Debug.Log("£A£A£A 3");
                    tutorial_rem_cash += 30;
                    selected[selindex] = false;
                }
                else if (selindex == 2)
                {
                    tutorial_rem_cash += 50;
                    selected[selindex] = false;
                }
                else if (selindex == 3)
                {
                    tutorial_rem_cash += 30;
                    selected[selindex] = false;
                }
                else if (selindex == 4)
                {
                    tutorial_rem_cash += 50;
                    selected[selindex] = false;
                }
                else if (selindex == 6)
                {
                    tutorial_rem_cash += 40;
                    selected[selindex] = false;
                }
                else if (selindex == 7)
                {
                    tutorial_rem_cash += 400;
                    selected[selindex] = false;
                }
                else if (selindex == 5)
                {
                    tutorial_rem_cash += 100;
                    selected[selindex] = false;
                }
                else if (selindex == 8)
                {
                    tutorial_rem_cash += 100;
                    selected[selindex] = false;
                }
            }
        }
    }
}
