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

    //Should turn this into a constant we can access

    private bool textDisplaying;

    private void Awake()
    {
        StartCoroutine(BeginEvent());
        skipButton.SetActive(true);
    }

    private IEnumerator BeginEvent()
    {
        dialogueAnim.SetBool("fadein", true);
        for (int i = 0; i < eventData.DialogueToDisplay.Length; i++)
        {
            displaySprite.sprite = eventData.SpritesToDisplay[i];
            StartCoroutine(ShowText(eventData.DialogueToDisplay[i]));

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
            sceneLoader.LoadSceneByIndex(sceneIndexToLoad);
            yield break;
        }
        yield return new WaitForEndOfFrame();

        //since we aren't loading another scene yet, this is the end game
        sceneLoader.LoadMainMenu();
    }

    private void ClearText()
    {
        displayText.text = "";
    }
}
