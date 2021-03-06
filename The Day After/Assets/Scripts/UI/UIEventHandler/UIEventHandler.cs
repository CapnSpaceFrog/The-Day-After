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
    [SerializeField] private Animator sceneLoaderAnim;
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
    [SerializeField]
    private Animator preGameplayAnim;

    private List<string> dialogueToDisplay;
    private List<Sprite> spritesToDisplay;

    [Header("End Dialogue")]
    [SerializeField]
    private Animator endDialogueAnim;
    [SerializeField]
    private string[] endDialogue;
    [SerializeField]
    private TextMeshProUGUI endText;
    [SerializeField]
    private Animator continueAnim;

    private bool textDisplaying;

    [Header("Post Game Anims")]
    public Animator playerAnim;
    public Animator momAnim;
    public Animator cameraAnim;

    private void Awake()
    {
        OverrideDisplay(eventData.DialogueToDisplay, eventData.SpritesToDisplay);

        if (StaticGameData.CompletedWithinTime == true)
        {
            OverrideDisplay(eventData.FinishedInTimeDialogue, eventData.FinishedInTimeSpriteDisplay);
        } 

        StartCoroutine(BeginEvent());

        skipButton.SetActive(true);
    }

    private IEnumerator BeginEvent()
    {
        yield return new WaitForSeconds(1.25f);
        dialogueAnim.GetComponent<Canvas>().sortingOrder = 6;
        dialogueAnim.SetBool("fadein", true);

        for (int i = 0; i < dialogueToDisplay.Count; i++)
        {
            displaySprite.sprite = spritesToDisplay[i]; 
            StartCoroutine(ShowText(dialogueToDisplay[i]));
            //Fade in at this certain dialogue mark if its the pre event
            if (eventData.IsPreEvent && dialogueToDisplay[i] == dialogueToDisplay[3])
            {
                sceneLoaderAnim.Play("SCENELOADER_FADEOUT");
                preGameplayAnim.Play("PreGameplayEvent");
                yield return new WaitForSeconds(sceneLoaderAnim.GetCurrentAnimatorStateInfo(0).length + 0.25f);
                dialogueAnim.GetComponent<Canvas>().sortingOrder = 4;
            }

            if (!eventData.IsPreEvent && dialogueToDisplay[i] == dialogueToDisplay[3])
            {
                sceneLoaderAnim.Play("SCENELOADER_FADEOUT");
                cameraAnim.Play("CAMERA_MOVE");
                playerAnim.Play("PLAYER_STARTMOVE");
                yield return new WaitForSeconds(sceneLoaderAnim.GetCurrentAnimatorStateInfo(0).length + 0.25f);
                dialogueAnim.GetComponent<Canvas>().sortingOrder = 4;
            }

            if (!eventData.IsPreEvent && StaticGameData.CompletedWithinTime && dialogueToDisplay[i] == dialogueToDisplay[19])
            {
                playerAnim.Play("PLAYER_MOVETOHUG");
            }

            if (!eventData.IsPreEvent && !StaticGameData.CompletedWithinTime && dialogueToDisplay[i] == dialogueToDisplay[12])
            {
                playerAnim.Play("PLAYER_MOVETOHUG");
            }

            //Wait for "Show Text" to finish before continuing
            yield return new WaitUntil(() => textDisplaying == false);

            if (!eventData.IsPreEvent && dialogueToDisplay[i] == dialogueToDisplay[5])
            {
                momAnim.Play("MOM_DROPBAGS");
            }
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

        Debug.Log("Reached End Dialogue Display");
        dialogueAnim.SetBool("fadein", false);

        StartCoroutine(FinishedScene());
    }


    private IEnumerator FinishedScene()
    {
        if (loadSceneAfterFinish)
        {
            Debug.Log("Loading Gameplay");
            yield return new WaitForSeconds(2f);
            sceneLoader.LoadSceneByIndex(sceneIndexToLoad);
            yield break;
        }

        //Not loading a scene, so this is the "end game" scene
        StartCoroutine(EndGameFinish());
    }

    private IEnumerator EndGameFinish()
    {
        yield return new WaitForSeconds(3.5f);
        sceneLoaderAnim.Play("SCENELOADER_FADEIN");
        yield return new WaitForSeconds(5f);

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
        displaySprite.sprite = Resources.Load<Sprite>("Sprites/Empty");
    }
}
