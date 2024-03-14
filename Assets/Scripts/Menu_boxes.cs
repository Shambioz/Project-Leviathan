using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public enum ItemType
{
    Test,
    TheBox
}
public class Menu_boxes : MonoBehaviour
{
    public ItemType type;
    bool flag = false;

    public void Interaction()
    {
        if (type == ItemType.TheBox)
        {
            flag = !flag;
            GetComponentInParent<Animator>().SetBool("BoxOpen", flag);
            GetComponentInParent<Animator>().SetBool("BoxClose", !flag);
        }

        if (type == ItemType.Test)
        {
            Destroy(gameObject);
        }
    }
}
