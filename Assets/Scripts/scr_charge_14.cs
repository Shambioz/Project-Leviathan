using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_charge_14 : MonoBehaviour
{
    public GameObject charge_14;
    public GameObject player;
    public Collider charge_14_collider;
    public Collider player_collider;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("drone_v2");
        charge_14 = GameObject.Find("charge_14_box_collider");
        charge_14_collider = charge_14.GetComponent<Collider>();
        player_collider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (charge_14_collider.bounds.Intersects(player_collider.bounds))
        {

        }
    }
}
