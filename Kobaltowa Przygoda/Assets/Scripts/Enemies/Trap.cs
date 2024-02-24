using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int trapLevel = 1;
    public float radius = 2f;
    private List<Enemy> enemies = new();

    private void Start() {
        foreach (var g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(g.GetComponent<Enemy>());
        }
    }

    private void Update() {
        foreach (var e in enemies) {
            if(Vector3.Distance(e.transform.position,transform.position) < radius)
            {
                e.StunEnemy(trapLevel);
                Destroy(this.gameObject);
            }
        }
    }
}
