using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_kick_thief_event : MonoBehaviour
{

    public GameObject thief;
    public Collider eventcollider;
    private Collider thiefcollider;
    public int button = 0;


    // Start is called before the first frame update
    void Start()
    {
        thiefcollider = thief.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventcollider.bounds.Intersects(thiefcollider.bounds) && button == 1)
        {
            Destroy(thief);
            Debug.Log("destroyed?");
        }
    }
}