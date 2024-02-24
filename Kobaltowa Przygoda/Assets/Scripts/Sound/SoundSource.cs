using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource _audio;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Volume")) _audio.volume = PlayerPrefs.GetFloat("Volume");
        else PlayerPrefs.SetFloat("Volume",1f);
    }

    // Update is called once per frame
    private void Update()
    {
        _audio.volume = PlayerPrefs.GetFloat("Volume");
    }
}
