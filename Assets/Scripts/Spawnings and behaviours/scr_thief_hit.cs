using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_thief_hit : MonoBehaviour
{
    private bool knocked_out = false;
    public scr_thief_ai thief_ai;
    public bool is_hit = false;
    private int maxhp = 500;
    public NavMeshAgent agent;
    public int hp = 0;
    public bool help1 = false;
    public scr_pick_up_object pick_up;
    public bool fix_this_shyt = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (is_hit == true)
        {
            hp -= 3;
            is_hit = false;
            if (hp < 0)
            {
                hp = 0;
            }
        }
        if (hp <= 0)
        {
            //if (agent == thief_ai.agent)
            //{
            //thief_ai.stateagent = 1;
            //Debug.Log("WAGABONGO");
            //}
        }

        //Debug.Log("THE TRUTH: " + help1 + " " + thief_ai.stateagent);

        /*if (help1 == true && thief_ai.stateagent == 1)
        {
            pick_up.hilfe1 = true;
            Debug.Log("WORKS");
        }
        help1 = false;*/

    }
}