using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class hungryManager : MonoBehaviour
{

  
   private List<GameObject> kidObjects;

   public List<Kid> kids = new List<Kid>();
   // Getter dla listy kidObjects
   public float avarageHunger;

  
   public KidsMaster _KidsMaster;

   void Start()
   {
      // Pobierz listę kidObjects z KidListManager
      kids = _KidsMaster.KidsList;

      // Wyświetl liczbę obiektów KID w konsoli
      Debug.Log("Liczba obiektów KID z innego skryptu: " + kids.Count);
      StartCoroutine(HungryCountdown());
   }

   private void Update()
   {
      kids = _KidsMaster.KidsList;
   }

   void hungry()
   {
      float helper = 0;
      foreach (Kid kid in kids)
      {
         Kid kidScript = kid.GetComponent<Kid>();
         helper += kidScript.hungry;

      }

      avarageHunger = helper / kids.Count;
   }

   void eat(int satiety)
   {
      avarageHunger += satiety;
   }

   void hungerDeath()
   {
      if (avarageHunger < 0)
      {
         List<Kid> kidsToRemove = new List<Kid>();

         foreach (Kid kid in kids)
         {
            int szansa = Random.Range(1, 100);

            if(szansa < (-Mathf.FloorToInt(avarageHunger)))
            {
               kidsToRemove.Add(kid);
            }
         }

         // Usuń dzieci z oryginalnej listy
         foreach (Kid kidToRemove in kidsToRemove)
         {
            _KidsMaster.DestroyChild(kidToRemove);
            kids.Remove(kidToRemove);
         }
      }
   }


  

   IEnumerator HungryCountdown()
   {
      while (true) {
         if (kids.Count > 0)
         {
             avarageHunger -=((kids.Count/10));
         }
        
         Debug.Log("Aktualna wartość: " + avarageHunger);
         hungerDeath();
         yield return new WaitForSeconds(1f);
      }
   }
}