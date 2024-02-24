using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class backpack : MonoBehaviour
{
    public float money;
    public int kobalAmount;
    public GameObject shop;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (shop.active)
            {
                shop.SetActive(false);
                    
            }
            else
            {
                shop.SetActive(true);
            }
        }
    }
}
