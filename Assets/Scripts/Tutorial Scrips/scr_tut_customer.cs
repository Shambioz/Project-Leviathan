using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class scr_tut_customer : MonoBehaviour
{

    public bool shot = false;
    private int cunter = 0;
    public GameObject targetto = null;
    private NavMeshAgent agento = null;
    private Collider col1 = null;
    private Collider col2 = null;

    // Start is called before the first frame update
    void Start()
    {
        shot = false;
        agento = this.GetComponent<NavMeshAgent>();
        col1 = this.GetComponent<Collider>();
        col2 = targetto.GetComponent<Collider>();
        cunter = 500;
        agento.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shot == true && cunter < 0)
        {
            agento.enabled = true;
            agento.SetDestination(targetto.transform.position);
            if (col1.bounds.Intersects(col2.bounds))
            {
                Destroy(this.GameObject());
            }
        }
        else if (shot == true)
        {
            cunter--;
            shot = false;
        }
    }
}
