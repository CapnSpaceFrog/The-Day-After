using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueHandler : MonoBehaviour
{
    private InterObj currentInterObj;

    private Animator anim;
    private TextMeshProUGUI displayText;
    private Image spriteDisplay;

    private bool textDisplaying;

    [SerializeField]
    private float textDisplayDelay;

    public List<string> dialogueToDisplay;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteDisplay = transform.Find("Sprite To Display").GetComponent<Image>();
        displayText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        //Test Dialgoue
        spriteDisplay.sprite = Resources.Load<Sprite>("Sprites/OutlineEmpty");
    }

    //needs to receive inter obj item from player interact script
    public void InitializeDialogue(InterObj objToReference)
    {
        currentInterObj = objToReference;
        dialogueToDisplay = currentInterObj.Obj_Data.DisplayDialogue;

        GameObject.FindGameObjectWithTag("Player").SendMessage("UpdateDialogueBool");

        anim.SetBool("fadein", true);
        BeginDialogueDisplay();
    }

    //Once all variables are set, Begin to display the dialogue
    private void BeginDialogueDisplay()
    {
        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        //Display dialogue in for loop and await user input to continue dialogue
        for (int i = 0; i < dialogueToDisplay.Count; i++)
        {
            //How are we going to feed in different dialogue strings depending on interaction?
            StartCoroutine(ShowText(dialogueToDisplay[i]));

            //Wait for "Show Text" to finish before continuing
            yield return new WaitUntil(() => textDisplaying == false);

            //Disply "press space to continue" once dialogue has displayed
            
            yield return WaitForKeyPress(Key.Space);
            //Proceed to next stage of text only until key is pressed
        }
        EndDialogueDisplay();
    }

    private IEnumerator ShowText(string fullText)
    {
        string currentText;
        textDisplaying = true;
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            displayText.text = currentText;
            yield return new WaitForSeconds(textDisplayDelay);
        }
        textDisplaying = false;
    }

    private IEnumerator WaitForKeyPress(Key key)
    {
        yield return new WaitUntil(() => Keyboard.current[key].wasPressedThisFrame);
    }

    private void EndDialogueDisplay()
    {
        StopAllCoroutines();
        GameObject.FindGameObjectWithTag("Player").SendMessage("UpdateDialogueBool");

        anim.SetBool("fadein", false);

        //If this object was a quest item, inform the game manager that we've finished the dialogue and an event can now occur
    }

    private void ClearTextDisplay()
    {
        displayText.text = "";
    }

    private void QuestDialogueAdd(string[] dialogueToAdd)
    {
        Debug.Log("added quest finish dialogue");
        for (int i = 0; i < dialogueToAdd.Length; i++)
        {
            dialogueToDisplay.Add(dialogueToAdd[i]);
        }
    }

    /*
    Dialogue display box needs to display
    A) Update player sprite based on dialogue string
    B) Inform game manager of quest completion and to progress quest requirements
    */
}
