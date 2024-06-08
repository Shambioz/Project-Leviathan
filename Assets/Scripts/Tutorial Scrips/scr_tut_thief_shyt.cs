using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class scr_tut_thief_shyt : MonoBehaviour
{

    public int hp = 500;
    public int timer = 500;
    public bool shot_or_not = false;
    public bool picked_up = false;
    public GameObject targetto = null;
    private NavMeshAgent agento = null;
    private Collider col1 = null;
    private Collider col2 = null;
    public scr_pickupable snauhnsd;

    // Start is called before the first frame update
    void Start()
    {
        shot_or_not = false;
        agento = this.GetComponent<NavMeshAgent>();
        col1 = this.GetComponent<Collider>();
        col2 = targetto.GetComponent<Collider>();
        agento.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0 && shot_or_not == false)
        {
            shot_or_not = true;
        }
        else if (shot_or_not == true && timer > 0)
        {
           timer--;
        }
        else if (timer <= 0 && snauhnsd.picked == true)
        {
            picked_up = true;
        }
        if (picked_up == true)
        {
            agento.enabled = true;
            agento.SetDestination(targetto.transform.position);
            if (col1.bounds.Intersects(col2.bounds))
            {
                Destroy(this.GameObject());
            }
        }
    }
}
