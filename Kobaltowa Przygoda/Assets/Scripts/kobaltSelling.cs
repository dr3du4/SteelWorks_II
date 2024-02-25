using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class kobaltSelling : MonoBehaviour
{
    public int money;
    public int kobaltAmount;
    public int kurs = 0;
    float kursTimer = 0;

    private backpack plecak;

    public TMP_Text kursText;
    public TMP_Text moneyText;
    public TMP_Text coinPlecakText;
    public TMP_Text KobaltPlecakText;


    private int kobaltWPlecaku;
    private float coinyWPlecaku;
    
    // Start is called before the first frame update
    void Start()
    {
        plecak = DayManager.Instance.GetComponent<backpack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (plecak)
        {
            kobaltWPlecaku = plecak.cobaltTotal;
            coinyWPlecaku = plecak.money;

            
            money = kobaltAmount * kurs;

            kursText.text = kurs.ToString();
            KobaltPlecakText.text = kobaltWPlecaku.ToString();
            coinPlecakText.text = coinyWPlecaku.ToString();
            moneyText.text = money.ToString();
        }

        if (Time.time >= kursTimer)
        {
            kursTimer = Time.time + DayManager.Instance.priceChangeTime;
            NewKurs();
        }
    }

    public void NewKurs()
    {
        kurs = (Random.Range(15, 55));
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
        /*
       if(kobaltAmount>0)
       { 
             backpack plecak = DayManager.Instance.GetComponent<backpack>();
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
        */
        plecak.SellKobalt(kobaltAmount);
        plecak.AddCash(money);
        money = 0;
        kobaltAmount = 0;

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
