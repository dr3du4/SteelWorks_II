using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/TutorialObject")]
public class TutorialObject : ScriptableObject
{
    [SerializeField] public int index;
	[SerializeField] public string title;
    [SerializeField] public Sprite background;
    [SerializeField] [TextArea]public string text;
    [SerializeField] public int nextIndex = -1;
}
