using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public static TutorialSystem Instance { get; private set; }
    [SerializeField] private List<TutorialObject> tutorials = new();
    [SerializeField] private List<bool> wasDisplayed;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

		wasDisplayed = new List<bool>();
		for(int i=0;i<tutorials.Count;i++) wasDisplayed.Add(false);
    }


    public bool DisplayTutorial(int index) {
        if (wasDisplayed[index]) return false;
        
        TutorialObject t = tutorials.Find(a => a.index == index);
        //Display tutorial
        
        wasDisplayed[index] = true;
        return true;
    }
}
