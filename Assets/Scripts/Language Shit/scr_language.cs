using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class scr_language : MonoBehaviour
{
    public static bool english = true;

    public TMP_Text easy;
    public TMP_Text medium;
    public TMP_Text hart;
    public TMP_Text jsadabfhadbh;


    public void ButtonLanguageChange()
    {
        if (english == true)
        {
            english = false;
            easy.text = "Makkelijk";
            medium.text = "Gemiddeld";
            hart.text = "Moeilijk";
            jsadabfhadbh.text= "KLIK OP DE NAAM VAN HET LEVEL OM TE SPELEN";

        }
        else
        {
            english = true;
            easy.text = "Easy";
            medium.text = "Normal";
            hart.text = "Hard";
            jsadabfhadbh.text = "CLICK ON THE LEVEL'S NAME TO PLAY";
        }
    }
}
