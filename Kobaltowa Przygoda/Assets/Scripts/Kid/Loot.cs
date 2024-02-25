using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    public int cobalt = 0;
    /*
     * 50% for food
    */
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            float x = Random.Range(0f, 1f);
            AddCobalt(cobalt);
            if (x > 0.5f) AddFood();
        }

        Destroy(this.gameObject);
    }

    private void AddFood()
    { 
        DayManager.Instance.GetComponent<hungryManager>().eat(5);
    }
    
    private void AddCobalt(int k) {
        DayManager.Instance.GetComponent<backpack>().DeliverCobalt(k);
    }
}
