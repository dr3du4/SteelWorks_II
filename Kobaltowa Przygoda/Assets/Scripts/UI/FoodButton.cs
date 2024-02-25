using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodButton : SellingMainScript
{
   public hungryManager _HungryManager;
   public int satiety;

   public void buyFood()
   {
      _HungryManager.eat(satiety);
   }

}
