using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DubbingObject")]
public class DubbingObject : ScriptableObject
{
    [SerializeField] private AudioClip[] dialogue;

    public AudioClip GetClip(int id)
    {
        return dialogue[id];
    }

}
