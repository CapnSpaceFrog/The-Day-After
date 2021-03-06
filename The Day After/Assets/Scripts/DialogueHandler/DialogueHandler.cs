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

    [SerializeField] private Animator continueAnim;

    public List<string> dialogueToDisplay;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteDisplay = transform.Find("Sprite To Display").GetComponent<Image>();
        displayText = GetComponentInChildren<TextMeshProUGUI>();
    }

    //needs to receive inter obj item from player interact script
    public void InitializeDialogue(InterObj objToReference)
    {
        currentInterObj = objToReference;
        dialogueToDisplay = currentInterObj.Obj_Data.DisplayDialogue;

        if (!StaticGameData.IsDressed)
        {
            currentInterObj.Obj_Data.DisplaySprite = Resources.Load<Sprite>("Sprites/PJSprite");
        }
        else
        {
            currentInterObj.Obj_Data.DisplaySprite = Resources.Load<Sprite>("Sprites/ClothedSprite");
        }

        anim.SetBool("fadein", true);
        BeginDialogueDisplay();
    }

    //Once all variables are set, Begin to display the dialogue
    private void BeginDialogueDisplay()
    {
        GameObject.FindGameObjectWithTag("Game Manager").SendMessage("UpdateDialogueFinish", false);
        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        spriteDisplay.sprite = currentInterObj.Obj_Data.DisplaySprite;
        //Display dialogue in for loop and await user input to continue dialogue
        for (int i = 0; i < dialogueToDisplay.Count; i++)
        {
            StartCoroutine(ShowText(dialogueToDisplay[i]));
            //Wait for "Show Text" to finish before continuing
            yield return new WaitUntil(() => textDisplaying == false);

            //Disply "press space to continue" once dialogue has displayed

            continueAnim.Play("SUBMENU_FADEIN");
            yield return WaitForKeyPress(Key.Space);
            continueAnim.Play("SUBMENU_FADEOUT");
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
            yield return new WaitForSeconds(StaticGameData.CurrentDisplaySpeed);
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

        //Send message to post dialogue handler that the dialogue has finished

        anim.SetBool("fadein", false);

        //Tell the game manager that the dialogue has finished
        GameObject.FindGameObjectWithTag("Game Manager").SendMessage("UpdateDialogueFinish", true);
    }

    private void ClearTextDisplay()
    {
        displayText.text = "";
        spriteDisplay.sprite = Resources.Load<Sprite>("Sprites/Empty");
    }

    private void QuestDialogueAdd(string[] dialogueToAdd)
    {
        for (int i = 0; i < dialogueToAdd.Length; i++)
        {
            dialogueToDisplay.Add(dialogueToAdd[i]);
        }
    }
}
