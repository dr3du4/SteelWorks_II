using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsMaster : MonoBehaviour
{
    [SerializeField] private List<Kid> kids;
	[SerializeField]private List<Kid> kidsInRange;	

    private void Update() {
        if (Input.GetKeyDown("q")) {
            if (kids.Count > 0) {
                Kid k = kids[0];
                kids.RemoveAt(0);
                k.StopFollowing(transform.position);
            } 
        }

		if (Input.GetKeyDown("e")) {
            foreach(Kid k in kidsInRange) {
				kids.Add(k);
				k.StartFollowing();		
			}
			kidsInRange.Clear();
        }
    }
    
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Kid")){
            Kid k = other.GetComponent<Kid>();
			if(!kids.Contains(k) && !kidsInRange.Contains(k)) {
				kidsInRange.Add(k);
			}
        }
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Kid")){
            Kid k = other.GetComponent<Kid>();
			kidsInRange.Remove(k);
        }
	}
}
