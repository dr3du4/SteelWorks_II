using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyStats : MonoBehaviour
{
    public float efektywnosc;
    public backpack _backpack;
    
    public void buySomeStas()
    {
        _backpack.efektywnosc += efektywnosc;
    }
}
