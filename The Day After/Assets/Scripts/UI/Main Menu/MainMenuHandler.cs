using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    #region Animator Variables
    [SerializeField]
    private Animator[] subMenuAnims;
    [SerializeField]
    private Animator mainButtonPanelAnim;
    [SerializeField]
    private Button[] mainButtons;
    [SerializeField]
    private Animator meunPlayerAnim;
    [SerializeField]
    private Animator disclaimerAnim;


    private Animator currentMenu;
    #endregion

    #region Game Start Variables
    private bool playerInPosition = false;
    private bool textDisplaying = false;
    private bool dialogueFinished;

    [Header("Dialgoue Variables")]
    [SerializeField]
    private TextMeshProUGUI displayText;
    [SerializeField]
    private float textDisplayTime;
    [SerializeField]
    private float betweenTextTime;
    [SerializeField]
    private string[] dialogueToDisplay;

    [SerializeField]
    private Animator textDisplayAnim;
    [SerializeField] private GameObject dayAfterText;
    #endregion

    [SerializeField]
    private GameObject sceneLoader;
    [SerializeField]
    private GameObject skipButton;


    private void Awake()
    {
        sceneLoader.GetComponent<Animator>().Play("SCENELOADER_FADEOUT");
    }
    private IEnumerator OnGameStart()
    {
        UpdateMainButtonInteraction(false);
        mainButtonPanelAnim.Play("SUBMENU_FADEOUT");
        disclaimerAnim.Play("SUBMENU_FADEOUT");
        yield return new WaitForSeconds(3f);
        skipButton.SetActive(true);

        //Begin player move animation
        meunPlayerAnim.Play("PLAYER_MENUANIM");
        yield return new WaitUntil(() => playerInPosition == true);
        Debug.Log("player in position");

        StartCoroutine(DialogueDisplay());
        yield return new WaitUntil(() => dialogueFinished == true);
        SendMessageToScreenLoader();
    }

    private IEnumerator DialogueDisplay()
    {
        for (int i = 0; i < dialogueToDisplay.Length; i++)
        {
            textDisplayAnim.SetBool("fadeout", false);

            StartCoroutine(ShowText(dialogueToDisplay[i]));

            if (dialogueToDisplay[i] == dialogueToDisplay[5])
            {
                dayAfterText.SetActive(true);
            }
            
            yield return new WaitUntil(() => textDisplaying == false);
        }
        dialogueFinished = true;
    }

    private IEnumerator ShowText(string textToDisplay)
    {
        textDisplaying = true;
        displayText.text = textToDisplay;

        textDisplayAnim.Play("SUBMENU_FADEIN");

        yield return new WaitForSeconds(textDisplayTime);

        textDisplayAnim.Play("SUBMENU_FADEOUT");
        yield return new WaitForSeconds(betweenTextTime);

        textDisplaying = false;
    }

    private void SendMessageToScreenLoader()
    {
        //Send a message to the scene loader to transition scenes
        sceneLoader.SendMessage("LoadPreGameplay");
        Debug.Log("Moving to Gameplay Scene");
    }

    private void DisplayMenuTab(string menuToDisplay)
    {
        UpdateMainButtonInteraction(false);

        switch (menuToDisplay)
        {
            case "tutorial":
                subMenuAnims[0].Play("SUBMENU_FADEIN");
                mainButtonPanelAnim.Play("SUBMENU_FADEOUT");
                currentMenu = subMenuAnims[0];
                //Display tutorial menu
                break;

            case "credits":
                subMenuAnims[1].Play("SUBMENU_FADEIN");
                mainButtonPanelAnim.Play("SUBMENU_FADEOUT");
                currentMenu = subMenuAnims[1];
                //Display credits menu
                break;

            case "settings":
                subMenuAnims[2].Play("SUBMENU_FADEIN");
                mainButtonPanelAnim.Play("SUBMENU_FADEOUT");
                currentMenu = subMenuAnims[2];
                //Display settings menu
                break;

            case "back":
                Debug.Log(currentMenu);
                currentMenu.Play("SUBMENU_FADEOUT");
                mainButtonPanelAnim.Play("SUBMENU_FADEIN");
                UpdateMainButtonInteraction(true);
                //Close current menu and display main menu
                break;
        }
    }

    #region On Button Press Functions
    public void OnPlayPress()
    {
        StartCoroutine(OnGameStart());
    }

    public void OnTutorialPress()
    {
        DisplayMenuTab("tutorial");
    }

    public void OnCreditsPress()
    {
        DisplayMenuTab("credits");
    }

    public void OnSettingsPress()
    {
        DisplayMenuTab("settings");
    }

    public void OnBackPress()
    {
        DisplayMenuTab("back");
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnSkipPress()
    {
        SendMessageToScreenLoader();
    }

    public void HandleInput(int val)
    {
        switch (val)
        {
            case 0:
                StaticGameData.CurrentDisplaySpeed = StaticGameData.SlowTextSpeed;
                Debug.Log(StaticGameData.CurrentDisplaySpeed);
                break;

            case 1:
                StaticGameData.CurrentDisplaySpeed = StaticGameData.MediumTextSpeed;
                Debug.Log(StaticGameData.CurrentDisplaySpeed);
                break;

            case 2:
                StaticGameData.CurrentDisplaySpeed = StaticGameData.FastTextSpeed;
                Debug.Log(StaticGameData.CurrentDisplaySpeed);
                break;
        }
    }

    private void UpdateMainButtonInteraction(bool ifInteractable)
    {
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].interactable = ifInteractable;
        }
    }
    #endregion

    private void PlayerInPosition() => playerInPosition = true;
}
