using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class scr_score_control : MonoBehaviour

{
    public scr_pick_up_object scr_pick_up_object;

    public int score = 0;
    public int mortar = 0;
    public int Soldier = 0;
    public int Armor = 0;
    public int SilverCup = 0;
    public int Morian = 0;
    public int Archeological = 0;
    public int BreadCoins = 0;
    public int Skull = 0;
    public int Canon = 0;

    private bool Pmortor = false;
    private bool Psoldier = false;
    private bool Parmor = false;
    private bool PSilverCup = false;
    private bool PMorian = false;
    private bool PArcheological = false;
    private bool PBreadcoins = false;
    private bool PSkull = false;
    private bool PCanon = false;


    public Light lightmortor;
    public Light lightsoldier;
    public Light lightarmor;
    public Light lightsilvercup;
    public Light lightmorian;
    public Light lightarcheological;
    public Light lightbreadcoin;
    public Light lightskull;
    public Light lightcanon;

    public string targetObjectName;

    public int LightStrength = 750;

    public TextMeshProUGUI scoreTable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        targetObjectName = scr_pick_up_object.obj_carried.name;

        SetBoolBasedOnName();

        if (mortar == 1)
        {
            if (Pmortor == true)
            {
                lightmortor.intensity = LightStrength;
            }
        }else { lightmortor.intensity = 0;}

        if (Skull == 1 && PSkull == true)
        {
            lightskull.intensity = LightStrength;
        }else { lightskull.intensity = 0; }


        //score = mortar + Soldier + Armor + SilverCup + Morian + Archeological + BreadCoins + Skull;
        //scoreTable.text = "Score: " + score;
    }

    public void SetBoolBasedOnName()
    {
        if (targetObjectName != null)
        {
            Pmortor = false;
            Psoldier = false;
            Parmor = false;
            PSilverCup = false;
            PMorian = false;
            PArcheological = false;
            PBreadcoins = false;
            PSkull = false;
            PCanon = false;
            // Example of setting bool variables based on the name
            switch (targetObjectName)
                {
                    case "Mortar of pharmacist Ellerus de Witt":
                        Pmortor = true;
                        break;
                    case "Johnny Skull":
                        PSkull = true;
                        break;
                    case "armor":
                        Parmor = true;
                        break;
                    // Add more cases as needed
                    default:
                        Debug.LogWarning("No matching case for the target object's name.");
                        break;
                }
        }
        else
        {
            Debug.LogError("BoolHandler component not found on the target object.");
        }
    }
}

