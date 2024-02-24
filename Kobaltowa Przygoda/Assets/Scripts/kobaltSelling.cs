using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class kobaltSelling : MonoBehaviour
{
    public float money;
    public int kobaltAmount;
    public float kurs = 0;
    public GameObject eq;
    
    public TMP_Text kursText;
    public TMP_Text moneyText;
    public TMP_Text coinPlecakText;
    public TMP_Text KobaltPlecakText;


    private int kobaltWPlecaku;
    private float coinyWPlecaku;
    
    // Start is called before the first frame update
    void Start()
    {
        backpack plecak = eq.GetComponent<backpack>();
        
            if (eq != null)
            {
                kobaltWPlecaku = plecak.kobalAmount;
                coinyWPlecaku = plecak.money;

                kurs = (Random.Range(212, 482)) / 100f;
                money = kobaltAmount * kurs;

                kursText.text = kurs.ToString();
                KobaltPlecakText.text = kobaltWPlecaku.ToString();
                coinPlecakText.text = coinyWPlecaku.ToString();
                moneyText.text = money.ToString();
            }
            else
            {
                Debug.LogError("Obiekt eq nie został przypisany w komponencie kobaltSelling.");
            }
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void updateTrade()
    {
        money = kobaltAmount * kurs;
        Debug.Log("Updated money: " + money);
        moneyText.text = money.ToString();
    }

    public void AddKobaltToSell()
    {
        Debug.Log(kobaltAmount);
        if (kobaltAmount < kobaltWPlecaku)
        {
            kobaltAmount += 1;
            updateTrade();
        }
        
    }

    
    public void Buy()
    {
       if(kobaltAmount>0)
       { backpack plecak = eq.GetComponent<backpack>();
             kobaltWPlecaku = kobaltWPlecaku - kobaltAmount;
                    coinyWPlecaku += money;
                    plecak.money += money; 
       }

       money = 0;
       kobaltAmount = 0;
       kursText.text = kurs.ToString();
       KobaltPlecakText.text = kobaltWPlecaku.ToString();
       coinPlecakText.text = coinyWPlecaku.ToString();
       moneyText.text = money.ToString();


    }
    public void OddKobaltToSell()
    {
        Debug.Log("ODD");
        if (kobaltAmount > 1)
        {
            kobaltAmount -= 1;
            updateTrade();
        }
        
    }
}
