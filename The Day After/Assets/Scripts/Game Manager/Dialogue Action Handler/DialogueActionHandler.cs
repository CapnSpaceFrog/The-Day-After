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

    public void CheckWhatToUpdate(InterObj objToUpdate)
    {
        //For this game, only QuestEvents trigger updates, but can be changed for other types
        //Could call a "NeedToUpdate" function to check through a switch statement
        switch (objToUpdate.Obj_Data.WhatToUpdate)
        {
            case UpdateType.none:
                break;

            case UpdateType.itself:
                UpdateObj(objToUpdate);
                break;

            case UpdateType.player:
                UpdatePlayer();
                break;

            case UpdateType.both:
                UpdateObj(objToUpdate);
                UpdatePlayer();
                break;
        }
    }

    public void CheckWhenToUpdate(InterObj objToUpdate)
    {
        switch (objToUpdate.Obj_Data.WhenToUpdate)
        {
            case UpdateTime.before:
                Debug.Log("Switch Before Dialogue");
                CheckWhatToUpdate(objToUpdate);
                break;

            case UpdateTime.after:
                Debug.Log("Switch After Dialogue");
                gm.StartCoroutine(UpdateAfterDialogue());
                break;

            case UpdateTime.both:
                Debug.Log("Switch Before and After Dialogue");
                break;
        }
    }

    private void UpdatePlayer()
    {
        Debug.Log("Updated Player");
    }

    private void UpdateObj(InterObj objToUpdate)
    {
        //Do we need to play an animation?

        //Do we need to update a collider?
        Debug.Log("Updated Obj");
    }

    private IEnumerator UpdateAfterDialogue()
    {
        yield return new WaitUntil(() => dialogueFinished == true);
        Debug.Log("Dialogue Finished Trigger");
    }

    public void UpdateDialogueFinish(bool isDialogueFinished)
    {
        dialogueFinished = isDialogueFinished;

        if (dialogueFinished == true)
        {
            playerRef.UpdateBusyBool();
        }
    }
}
