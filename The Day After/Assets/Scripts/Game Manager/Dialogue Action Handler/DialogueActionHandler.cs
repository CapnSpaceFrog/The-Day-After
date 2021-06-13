using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActionHandler
{
    private Player playerRef;
    private GameManager gm;

    private InterObj currentInterObj;

    public bool dialogueFinished;


    public DialogueActionHandler(Player playerRef, GameManager gm)
    {
        this.playerRef = playerRef;
        this.gm = gm;
    }

    public void CheckWhatToUpdate(string[] animToPlay)
    {
        switch (currentInterObj.Obj_Data.WhatToUpdate)
        {
            case UpdateType.itself:
                UpdateObj(animToPlay[1]);
                break;

            case UpdateType.player:
                UpdatePlayer(animToPlay[0]);
                break;

            case UpdateType.both:
                UpdateObj(animToPlay[1]);
                UpdatePlayer(animToPlay[0]);
                break;
        }
    }

    public void CheckWhenToUpdate(InterObj objToUpdate)
    {
        currentInterObj = objToUpdate;
        string[] animsToPlay = new string[2];

        switch (currentInterObj.Obj_Data.WhenToUpdate)
        {
            case UpdateTime.none:
                break;

            case UpdateTime.before:
                animsToPlay[0] = currentInterObj.Obj_Data.BeforeAnimsToPlay[0];
                animsToPlay[1] = currentInterObj.Obj_Data.BeforeAnimsToPlay[1];
                CheckWhatToUpdate(animsToPlay);
                break;

            case UpdateTime.after:
                animsToPlay[0] = currentInterObj.Obj_Data.AfterAnimsToPlay[0];
                animsToPlay[1] = currentInterObj.Obj_Data.AfterAnimsToPlay[1];
                gm.StartCoroutine(UpdateAfterDialogue(animsToPlay));
                break;

            case UpdateTime.both:
                animsToPlay[0] = currentInterObj.Obj_Data.BeforeAnimsToPlay[0];
                animsToPlay[1] = currentInterObj.Obj_Data.BeforeAnimsToPlay[1];
                CheckWhatToUpdate(animsToPlay);
                animsToPlay[0] = currentInterObj.Obj_Data.AfterAnimsToPlay[0];
                animsToPlay[1] = currentInterObj.Obj_Data.AfterAnimsToPlay[1];
                gm.StartCoroutine(UpdateAfterDialogue(animsToPlay));
                break;
        }
    }

    private IEnumerator UpdateAfterDialogue(string[] animsToPlay)
    {
        yield return new WaitUntil(() => dialogueFinished == true);
        CheckWhatToUpdate(animsToPlay);
    }

    private void UpdatePlayer(string animToPlay)
    {
        playerRef.AnimManager.ChangeAnimationState(animToPlay);
    }

    private void UpdateObj(string animToPlay)
    {
        currentInterObj.ChangeAnimationState(animToPlay);
    }

    #region Misc Methods
    public void UpdateDialogueFinish(bool isDialogueFinished)
    {
        dialogueFinished = isDialogueFinished;

        if (dialogueFinished == true)
        {
            playerRef.UpdateBusyBool();
        }
    }
    #endregion
}
