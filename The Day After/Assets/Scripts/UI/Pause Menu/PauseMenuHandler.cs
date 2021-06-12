using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private Animator anim;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private PlayerInputHandler inputHandler;

    private void Awake()
    {
        canvasGroup.interactable = false;
    }

    private void Update()
    {
        //Better method of implementation is to call a pause menu function on input instead of checking for this if statement every frame
        if (inputHandler.PauseInput) {
            inputHandler.UsePauseInput();
            if (canvasGroup.interactable == false) {
                OpenMenu();
            } else {
                CloseMenu();
            }
        }
    }

    public void HandleInput(int val)
    {
        switch (val)
        {
            case 0:
                StaticGameData.CurrentDisplaySpeed = StaticGameData.SlowTextSpeed;
                Debug.Log("Slow " + StaticGameData.CurrentDisplaySpeed);
                break;

            case 1:
                StaticGameData.CurrentDisplaySpeed = StaticGameData.MediumTextSpeed;
                Debug.Log("Medium " + StaticGameData.CurrentDisplaySpeed);
                break;

            case 2:
                StaticGameData.CurrentDisplaySpeed = StaticGameData.FastTextSpeed;
                Debug.Log("Fast " + StaticGameData.CurrentDisplaySpeed);
                break;
        }
    }

    private void OpenMenu()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        anim.Play("SUBMENU_FADEIN");
    }

    private void CloseMenu()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        anim.Play("SUBMENU_FADEOUT");
    }

    public void OnClosePress()
    {
        CloseMenu();
    }

    public void OnRetryPress()
    {
        sceneLoader.LoadGameplay();
    }

    public void OnMainMenuPress()
    {
        sceneLoader.LoadMainMenu();
    }
}
