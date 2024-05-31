using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_score_control : MonoBehaviour

{
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

    public TextMeshProUGUI scoreTable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = mortar + Soldier + Armor + SilverCup + Morian + Archeological + BreadCoins + Skull;
        scoreTable.text = "Score: " + score;
    }
}
