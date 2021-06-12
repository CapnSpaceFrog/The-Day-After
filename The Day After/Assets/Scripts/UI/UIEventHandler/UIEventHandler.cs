using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEventHandler : MonoBehaviour
{
    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private GameObject skipButton;
    [SerializeField]
    private UIEventData eventData;

    [Header("Dialogue Variables")]
    [SerializeField] private Animator dialogueAnim;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Image displaySprite;

    [SerializeField]
    private bool loadSceneAfterFinish;
    [SerializeField]
    private int sceneIndexToLoad;

    private List<string> dialogueToDisplay;
    private List<Sprite> spritesToDisplay;

    [Header("End Dialogue")]
    [SerializeField]
    private Animator endDialogueAnim;
    [SerializeField]
    private string[] endDialogue;
    [SerializeField]
    private TextMeshProUGUI endText;
    //Should turn this into a constant we can access

    private bool textDisplaying;

    private void Awake()
    {
        StartCoroutine(BeginEvent());
        if (StaticGameData.CompletedWithinTime == true)
        {
            OverrideDisplay(eventData.FinishedInTimeDialogue, eventData.FinishedInTimeSpriteDisplay);
        } else {
            OverrideDisplay(eventData.DialogueToDisplay, eventData.SpritesToDisplay);
        } 
        skipButton.SetActive(true);
    }

    private IEnumerator BeginEvent()
    {
        yield return new WaitForSeconds(1.25f);
        dialogueAnim.SetBool("fadein", true);
        for (int i = 0; i < dialogueToDisplay.Count; i++)
        {
            displaySprite.sprite = spritesToDisplay[i];
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

        dialogueAnim.SetBool("fadein", false);

        StartCoroutine(FinishedScene());
    }


    private IEnumerator FinishedScene()
    {
        if (loadSceneAfterFinish)
        {
            yield return new WaitForSeconds(2f);
            sceneLoader.LoadSceneByIndex(sceneIndexToLoad);
            yield break;
        }

        //Not loading a scene, so this is the "end game" scene
        StartCoroutine(EndGameFinish());
    }

    private IEnumerator EndGameFinish()
    {
        sceneLoader.GetComponent<Animator>().Play("SCENELOADER_FADEIN");
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < endDialogue.Length; i++)
        {
            endText.text = endDialogue[i];
            endDialogueAnim.Play("SUBMENU_FADEIN");
            yield return new WaitForSeconds(4f);
            endDialogueAnim.Play("SUBMENU_FADEOUT");
            yield return new WaitForSeconds(1.45f);
        }
        sceneLoader.LoadMainMenu();
    }

    private void OverrideDisplay(string[] stringOverride, Sprite[] spriteOverride)
    {
        dialogueToDisplay = new List<string>(new string[stringOverride.Length]);
        spritesToDisplay = new List<Sprite>(new Sprite[spriteOverride.Length]);
        for (int i = 0; i < stringOverride.Length; i++)
        {
            dialogueToDisplay[i] = stringOverride[i];
            spritesToDisplay[i] = spriteOverride[i];
        }
    }

    private void ClearText()
    {
        displayText.text = "";
    }
}
