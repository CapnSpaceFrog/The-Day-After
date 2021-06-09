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
        displayText.text = "test lorum ipsum ajbsdnaujbsd";
        spriteDisplay.sprite = Resources.Load<Sprite>("Sprites/OutlineEmpty");
    }

    //needs to receive inter obj item from player interact script
    public void InitializeDialogue(InterObj objToReference)
    {
        //Goal of method is to receive interobj and prepared the rest of the scripts variables to handle the data accordingly
        currentInterObj = objToReference;
        dialogueToDisplay = currentInterObj.Obj_Data.DisplayDialogue;

        Debug.Log($"Received Inter Obj: {currentInterObj}");
        //may need to create list, which we update each time we receive an inter object.
        //List expands and contracts based on what we need to display.

        BeginDialogueDisplay();
    }

    //Once all variables are set, Begin to display the dialogue
    private void BeginDialogueDisplay()
    {
        //Actiavte dialogue panel, trigger animations, and start coroutine to display data.

        //inform player script it is in dialogue and prevent certain actions
        GameObject.FindGameObjectWithTag("Player").SendMessage("UpdateDialogueBool");

        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        Debug.Log(dialogueToDisplay.Count);
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
        currentInterObj = null;
        //Renable player movement and hide dialogue box along with playing animation
        GameObject.FindGameObjectWithTag("Player").SendMessage("UpdateDialogueBool");
    }





    //Needs to take array of dialogue and display it
    //Needs to know which array of dialogue to display depending on inter object type

    /*
    Dialogue display box needs to display
    A) Display dialogue via type writer effect into box, 
    B) Update player sprite based on dialogue string
    B) Wait for user input before display the next string in dialogue display
    C) Send information back to inter object to let it know certain dialogue has been triggered

    */
}
