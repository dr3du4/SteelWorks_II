using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellingMainScript : MonoBehaviour
{
    public bool isRepetative;
    public int price;
    public TMP_Text priceText;
    public kobaltSelling kobaltowyManager;
    void Start()
    {
        priceText.text = price.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy()
    {
        if (price < kobaltowyManager.coinyWPlecaku)
        {
            kobaltowyManager.coinyWPlecaku -= price;
            if (!isRepetative)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
