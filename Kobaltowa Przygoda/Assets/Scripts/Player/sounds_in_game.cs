using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class sounds_in_game : MonoBehaviour
{
private Rigidbody2D rb;
private DepositController kopanie;
private BasicEnemy kidnaper;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kopanie = GetComponent<DepositController>();
        kidnaper = GetComponent<BasicEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rb && rb.velocity.magnitude>0) {
			StepsSound();
		}

        if (kopanie && kopanie.MiningStatus()){
            KopanieSound();
        }

        if (kidnaper && kidnaper.kidnapping){
            Krzykanie();
        }
    }

    void StepsSound(){
		AudioSource movement = GetComponent<AudioSource>();
        if (!movement.isPlaying){
		movement.Play();
        }
    //Debug.Log("tupu tupu tupu ");
}


void KopanieSound(){
    AudioSource stukstuk = GetComponent<AudioSource>();
    if (!stukstuk.isPlaying){
		stukstuk.Play();
        }
    Debug.Log("stukstuk ");
}

void Krzykanie(){
    AudioSource krzyk = GetComponent<AudioSource>();
    if (!krzyk.isPlaying){
		krzyk.Play();
        }
    Debug.Log("aaaaaaa ");
}

}
