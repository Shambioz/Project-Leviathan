using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_thief_hit : MonoBehaviour
{
    private bool knocked_out = false;
    public scr_thief_ai thief_ai;
    public bool is_hit = false;
    private int maxhp = 3000;
    public int hp = 0;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_hit == false && hp < maxhp)
        {
            hp++;
        }
        if (is_hit == true)
        {
            hp -= 3;
            if (hp > 0)
            {
                is_hit = false;
            }
        }
    }
}
