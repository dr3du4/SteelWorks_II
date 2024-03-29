using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class DialogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogObject testDialogue;
    [SerializeField] private DubbingObject testDubbing;
    [SerializeField] private GameObject dialogueBox;

    private AudioSource _audio;
    
    public bool IsOpen {get; private set; }
    
    private TypewritterEffect typewritterEffect;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
       // textLabel.text = "Hello!\n This is my new line.";
      // GetComponent<TypewritterEffect>().Run("Hellajhbfiluafiyugflo!\n This is maefahifuhay new line.", textLabel);
        typewritterEffect =GetComponent<TypewritterEffect>();
        //CloseDialogueBox();
        Debug.Log("Start");
        ShowDialogueChange(testDialogue);
    }

    private void ShowDialogueChange(DialogObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialougeChange(dialogueObject));
    }

    private IEnumerator StepThroughDialougeChange(DialogObject dialogueObject)
    {
        //yield return new WaitForSeconds(2);
        _audio.PlayOneShot(testDubbing.GetClip(0));
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewritterEffect.Run(dialogue, textLabel);
            yield return new WaitForSeconds(1f);
        }
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialougeChange(dialogueObject));
    }

    public IEnumerator StepThroughDialouge(DialogObject dialogueObject)
    {
        //yield return new WaitForSeconds(2);
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewritterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));

        }
        CloseDialogueBox();
    }

    private void CloseDialogueBox(){
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }
}
