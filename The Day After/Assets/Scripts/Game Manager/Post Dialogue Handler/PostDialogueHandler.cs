using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostDialogueHandler
{
    private Player playerRef;

    private InterObj currentInterObj;
    
    public PostDialogueHandler(Player playerRef)
    {
        this.playerRef = playerRef;
    }



    public void CheckIfShouldUpdate(InterObj objToUpdate)
    {
        //For this game, only QuestEvents trigger updates, but can be changed for other types
        //Could call a "NeedToUpdate" function to check through a switch statement
        switch (objToUpdate.Obj_Data.WhatToUpdate)
        {
            case UpdateType.none:
                ReturnPlayerControl();
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

    private void UpdatePlayer()
    {
        //Always end with sending control back to the player
        ReturnPlayerControl();
    }

    private void UpdateObj(InterObj objToUpdate)
    {
        //Do we need to play an animation?

        //Do we need to update a collider?
        throw new NotImplementedException("Update Obj not added yet");
    }

    private void ReturnPlayerControl() => playerRef.UpdateBusyBool();
}
