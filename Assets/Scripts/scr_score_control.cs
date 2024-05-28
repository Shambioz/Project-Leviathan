using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_score_control : MonoBehaviour

{
    public int score = 0;
    public int mortar = 0;
    public TextMeshProUGUI scoreTable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = mortar;
        scoreTable.text = "Score: " + score;
    }
}
