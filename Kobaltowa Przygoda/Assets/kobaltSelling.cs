using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class kobaltSelling : MonoBehaviour
{

    public int kobaltWPlecaku = 10;
    public float  coinyWPlecaku = 0;
    
    
    public int kobaltAmount=0;
    public float kurs = 0;
    public float money;
    
    
    
    public TMP_Text kursText;
    public TMP_Text moneyText;
    public TMP_Text coinPlecakText;
    public TMP_Text KobaltPlecakText;

    // Start is called before the first frame update
    void Start()
    {
        kurs = (Random.Range(212,482))/100f;
        money = kobaltAmount * kurs;
        
        
        kursText.text = kurs.ToString();
        KobaltPlecakText.text = kobaltWPlecaku.ToString();
        coinPlecakText.text = coinyWPlecaku.ToString();
        moneyText.text = money.ToString();

    }

    // Update is called once per frame
    void Update()
    {
         kursText.text = kurs.ToString();
                KobaltPlecakText.text = kobaltWPlecaku.ToString();
                coinPlecakText.text = coinyWPlecaku.ToString();
                moneyText.text = money.ToString();
    }

    public void updateTrade()
    {
        money = kobaltAmount * kurs;
       
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
       {
             kobaltWPlecaku = kobaltWPlecaku - kobaltAmount;
                    coinyWPlecaku += money;
       }

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
