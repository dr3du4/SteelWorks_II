using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsMaster : MonoBehaviour
{
    [SerializeField] private List<Kid> kids;

    private void Update() {
        if (Input.GetKeyDown("space")) {
            if (kids.Count > 0) {
                Kid k = kids[0];
                kids.RemoveAt(0);
                k.StopFollowing(transform.position);
            } 
        }
    }
    
}
