using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialSystem : MonoBehaviour
{
    public static TutorialSystem Instance { get; private set; }
    [SerializeField] private GameObject tutorialDisplay;
    [SerializeField] private Image tutorialBackground;
    [SerializeField] private Image titleBackground;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image describeBackground;
    [SerializeField] private TMP_Text describeText;
    
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
        ShowHideTutorial(false);
    }


    public void DisplayTutorial(int index) {
        TutorialObject t = tutorials.Find(a => a.index == index);
        
        //Display tutorial
        //SetImage
        tutorialBackground.sprite = t.background;
        ShowHideTutorial(true); //with clear texts
        //SetText
        titleText.text = t.title;
        describeText.text = t.text;
        
        wasDisplayed[index] = true;
    }

    public void ShowHideTutorial(bool show) {
        int x = 0;
        if (show) x = 255;

        Color c = tutorialBackground.color;
        tutorialBackground.color = new Color(c.r, c.g, c.b, x);
        
        c = titleBackground.color;
        titleBackground.color = new Color(c.r, c.g, c.b, x);
        
        c = describeBackground.color;
        describeBackground.color = new Color(c.r, c.g, c.b, x);

        titleText.text = "";
        describeText.text = "";

        tutorialDisplay.SetActive(show);
    }
}
