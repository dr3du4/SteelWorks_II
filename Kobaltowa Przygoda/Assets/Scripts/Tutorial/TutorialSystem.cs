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
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject nextButton;
    private int lastIndex = -1;

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

    public bool tutorialWas(int index) {
        return wasDisplayed[index];
    }
    
    public void DisplayTutorial(int index) {
        TutorialObject t = tutorials.Find(a => a.index == index);
        //Display tutorial
        ShowHideTutorial(true); //with clear texts
        //SetImage
        tutorialBackground.sprite = t.background;
        //SetText
        titleText.text = t.title;
        describeText.text = t.text;
        
        if (t.nextIndex == -1) closeButton.SetActive(true);
        else nextButton.SetActive(true);
        
        wasDisplayed[index] = true;
        lastIndex = index;
    }

    public void NextTutorial() {
        TutorialObject t = tutorials.Find(a => a.index == lastIndex);
        DisplayTutorial(t.nextIndex);
    }

    public void ShowHideTutorial(bool show) {
        float x = 0;
        if (show) {
            x = 200f/255f;
            Time.timeScale = 0f;
        }
        else Time.timeScale = 1f;
        
        Color c = tutorialBackground.color;
        tutorialBackground.color = new Color(c.r, c.g, c.b, 1);
        
        c = titleBackground.color;
        titleBackground.color = new Color(c.r, c.g, c.b, x);
        
        c = describeBackground.color;
        describeBackground.color = new Color(c.r, c.g, c.b, x);

        titleText.text = "";
        describeText.text = "";
        
        tutorialDisplay.SetActive(show);
        closeButton.SetActive(false);
        nextButton.SetActive(false);
    }
}
