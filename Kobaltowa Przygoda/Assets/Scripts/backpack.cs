using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpack : MonoBehaviour
{
    public int money;
    public int cobaltTotal;
    [SerializeField] private IngotAnim anim;

    public float efektywnosc;

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

    public bool Purchase(int value)
    {
        if (money - value < 0)
            return false;
        money -= value;
        return true;
    }

    public bool SellKobalt(int amount)
    {
        if (cobaltTotal - amount < 0)
            return false;
        cobaltTotal -= amount;
        return true;
    }

    public void AddCash(int amount)
    {
        money += amount;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            DeliverCobalt(100);
    }
}
