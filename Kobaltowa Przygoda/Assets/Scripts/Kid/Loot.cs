using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    public int kobalt = 0;
    /*
     * 50% for food
     * 1% for item
    */
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            float x = Random.Range(0f, 1f);
            AddKobalt(kobalt);
            if (x > 0.5f) AddFood();
            if (x > 0.99f) AddItem();
        }

        Destroy(this.gameObject);
    }

    private void AddFood() {
        Debug.Log("FOOD");
    }
    
    private void AddKobalt(int k) {
        Debug.Log("KOBALT");
    }

    private void AddItem() {
        Debug.Log("ITEM");
    }
}
