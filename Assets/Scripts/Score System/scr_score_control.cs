using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class scr_score_control : MonoBehaviour

{
    public scr_pick_up_object scr_pick_up_object;
    private scr_pickupable scr_pickupable;

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
    public int Sword = 0;

    private bool Pmortor = false;
    private bool Psoldier = false;
    private bool Parmor = false;
    private bool PSilverCup = false;
    private bool PMorian = false;
    private bool PArcheological = false;
    private bool PBreadcoins = false;
    private bool PSkull = false;
    private bool PCanon = false;
    private bool PSword = false;


    public Light lightmortor;
    public Light lightsoldier;
    public Light lightarmor;
    public Light lightsilvercup;
    public Light lightmorian;
    public Light lightarcheological;
    public Light lightbreadcoin;
    public Light lightskull;
    public Light lightcanon;
    public Light lightSword;

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

        targetObjectName = scr_pick_up_object.the_one_you_picked_up.name;
        scr_pickupable = scr_pick_up_object.the_one_you_picked_up.GetComponent<scr_pickupable>();
        SetBoolBasedOnName();
        switch (scr_pickupable.UniqueIdentifier)
        {
            case null:
                LightOff();
                break;

            case "mortor":
                if (scr_pickupable.is_in_place == false && scr_pickupable.picked == true)
                {
                    lightmortor.intensity = LightStrength;
                }
                else { lightmortor.intensity = 0;}
                break;
            case "armor":
                if (scr_pickupable.is_in_place == false && scr_pickupable.picked == true)
                {
                    lightarmor.intensity = LightStrength;
                }
                else { lightarmor.intensity = 0;}
                break;
            case "brit":
                if (scr_pickupable.is_in_place == false && scr_pickupable.picked == true)
                {
                    lightsoldier.intensity = LightStrength;
                }
                else { lightsoldier.intensity = 0; }
                break;
            case "Morian":
                if (scr_pickupable.is_in_place == false && scr_pickupable.picked == true)
                {
                    lightmorian.intensity = LightStrength;
                }
                else { lightmorian.intensity = 0; }
                break;
            case "Bread tokens from the Ritske Boelema Hospice":
                if (scr_pickupable.is_in_place == false && scr_pickupable.picked == true)
                {
                    lightbreadcoin.intensity = LightStrength;
                }
                else { lightbreadcoin.intensity = 0; }
                break;
            case "Sword of the past":
                if (scr_pickupable.is_in_place == false && scr_pickupable.picked == true)
                {
                    lightSword.intensity = LightStrength;
                }
                else { lightSword.intensity = 0; }
                break;
            case "Johnny Skull":
                if (scr_pickupable.is_in_place == false && scr_pickupable.picked == true)
                {
                    lightskull.intensity = LightStrength;
                }
                else { lightskull.intensity = 0; }
                break;

        }
        /*
        if (scr_pickupable.is_in_place == false && scr_pickupable.UniqueIdentifier == "mortor" && scr_pickupable.picked == true)
        {
            if (Pmortor == true)
            {
                lightmortor.intensity = LightStrength;
            }
        }else if (scr_pickupable.is_in_place == true && scr_pickupable.UniqueIdentifier == "mortor") { lightmortor.intensity = 0;}

        if (scr_pickupable.is_in_place == false && scr_pickupable.UniqueIdentifier == "skull" && scr_pickupable.picked == true)
        {
            if (PSkull == true) { lightskull.intensity = LightStrength; }

        }else if (scr_pickupable.is_in_place == true && scr_pickupable.UniqueIdentifier == "skull") { lightskull.intensity = 0; }

        if (scr_pickupable.is_in_place == false && scr_pickupable.UniqueIdentifier == "armor" && scr_pickupable.picked == true)
        {
            if (Parmor == true) { lightarmor.intensity = LightStrength; }
        }
        else if (scr_pickupable.is_in_place == true && scr_pickupable.UniqueIdentifier == "armor") { lightarmor.intensity = 0; }
        */


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
                    case "The soldier of Barrahus":
                        Psoldier = true;
                        break;
                    case "Morian":
                        PMorian = true;
                        break;
                    case "Bread tokens from the Ritske Boelema Hospice":
                        PBreadcoins = true;
                        break;
                    case "Sword of the past":
                        PSword = true;
                        break;
                // Add more cases as needed Bread tokens from the Ritske Boelema Hospice
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

    public void LightOff() 
    {
    lightmortor.intensity = 0;
    lightsoldier.intensity = 0;
    lightarmor.intensity = 0;
    lightsilvercup.intensity = 0;
    lightmorian.intensity = 0;
    lightarcheological.intensity = 0;
    lightbreadcoin.intensity = 0;
    lightskull.intensity = 0;
    lightcanon.intensity = 0;
    }
}

