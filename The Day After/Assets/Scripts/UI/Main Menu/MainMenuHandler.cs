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
    private Animator currentMenu;
    [SerializeField]
    private Animator meunPlayerAnim;
    #endregion

    #region Game Start Variables
    private bool playerInPosition = false;
    private bool textDisplaying = false;
    private bool dialogueFinished;

    [Header("Dialgoue Variables")]
    [SerializeField]
    private TextMeshProUGUI displayText;
    [SerializeField]
    private float textDisplayDelay;
    [SerializeField]
    private float betweenTextDisplayTime;
    [SerializeField]
    private string[] dialogueToDisplay;

    [SerializeField]
    private Animator textDisplayAnim;
    #endregion

    [SerializeField]
    private GameObject sceneLoader;
    [SerializeField]
    private GameObject skipButton;

    private IEnumerator OnGameStart()
    {
        UpdateMainButtonInteraction(false);
        mainButtonPanelAnim.Play("SUBMENU_FADEOUT");
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
            
            yield return new WaitUntil(() => textDisplaying == false);
        }
        dialogueFinished = true;
    }

    private IEnumerator ShowText(string textToDisplay)
    {
        textDisplaying = true;
        displayText.text = textToDisplay;

        textDisplayAnim.Play("SUBMENU_FADEIN");

        yield return new WaitForSeconds(betweenTextDisplayTime);

        textDisplayAnim.Play("SUBMENU_FADEOUT");
        yield return new WaitForSeconds(1.85f);

        textDisplaying = false;
    }

    private void SendMessageToScreenLoader()
    {
        //Send a message to the scene loader to transition scenes
        sceneLoader.SendMessage("LoadGameplay");
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
