using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpack : MonoBehaviour
{
    public float money;
    public int cobaltTotal;
    public GameObject myObject;
    [SerializeField] private IngotAnim anim;


    public void DeliverCobalt(Kid k)
    {
        if(k.holdCobalt > 0)
        {
            cobaltTotal += k.holdCobalt;
            k.holdCobalt = 0;
            anim.PlayAnim();
        }
    }
    
    public void DeliverCobalt(int k)
    {
        cobaltTotal += k;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            myObject.SetActive(!myObject.activeSelf);
        }
    }
}
