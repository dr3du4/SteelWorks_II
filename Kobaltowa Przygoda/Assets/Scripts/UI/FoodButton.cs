using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodButton : SellingMainScript
{
   hungryManager _HungryManager;
   public int satiety;

    private void Start()
    {
        _HungryManager = DayManager.Instance.GetComponent<hungryManager>();
    }

    public void buyFood()
   {
      _HungryManager.eat(satiety);
   }

}
