using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellingMainScript : MonoBehaviour
{
    public bool isRepetitive;
    public int price;
    public TMP_Text priceText;

    public GameObject eq;
    public TMP_Text moneyAmount;

    private float moneyFromBackpack;

    void Start()
    {
        priceText.text = price.ToString();
    }

    void Update()
    {
        
    }

    public void Buy()
    {
       
        
            backpack eqScript = eq.GetComponent<backpack>();
            moneyFromBackpack = eqScript.money;
        Debug.Log("Money in backpack before purchase: " + eqScript.money);
        moneyAmount.text = eqScript.money.ToString();
                
        if (price <= moneyFromBackpack)
        {
                    eqScript.money -= price; 
                Debug.Log("Money in backpack after purchase: " + eqScript.money);

                    if (!isRepetitive)
                    {
                        gameObject.SetActive(false);
                    }

                    // Zaktualizuj tekst w moneyAmount po zakupie (jeśli to konieczne)
                    moneyAmount.text = eqScript.money.ToString();
        }
              
    }
            
        
}