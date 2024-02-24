using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip soundOnMouseOver;
    [SerializeField] private AudioClip soundOnMouseClick;
    
    private AudioSource _audioHover;
    private AudioSource _audio;
    
    private void Start() {
        _audio = GetComponent<AudioSource>();
    }
    
    public void OnMouseOver()
    {
        if (soundOnMouseOver == null) return;
        _audio.PlayOneShot(soundOnMouseOver);
    }

    public void OnMouseClick() {
        if (soundOnMouseClick == null) return;
        _audio.PlayOneShot(soundOnMouseClick);
    }
}
