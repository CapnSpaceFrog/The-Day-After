using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    right,
    left
}
public class PlayerInteract
{
    private PlayerData p_data;
    private Player player;
    private GameManager gm;

    private InterObj currentInterObj;

    private bool sendToActionHandler;

    private FacingDirection direction;
    
    public Vector2 castPositionBotRight;
    public Vector2 castPositionTopLeft;
    public Transform hitbox;

    public FacingDirection Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    //Reminder to set to private when done testing
    public GameObject interactCheck;

    public PlayerInteract(Player player, PlayerData p_data, GameManager gm)
    {
        this.p_data = p_data;
        this.player = player;
        this.gm = gm;

        CreateInteractHitbox();
    }

    private void CreateInteractHitbox()
    {
        GameObject hitbox = new GameObject("Interact Hitbox");
        hitbox.transform.parent = player.transform;
        hitbox.transform.localPosition = p_data.HitBoxPos;
        this.hitbox = hitbox.transform;
    }

    public void UpdateInteractCastPosition()
    {
        //Depending on direction of player, update where the hit casts
        //Reminder to double check this distance once we are play testing inside the environment
        switch (direction)
        {
            case FacingDirection.right:
                hitbox.transform.localPosition = p_data.HitBoxPos;
                castPositionBotRight = new Vector2(hitbox.transform.position.x + (p_data.InteractBoxWidth / 2), hitbox.transform.position.y - (p_data.InteractBoxHeight / 2));
                castPositionTopLeft = new Vector2(hitbox.transform.position.x - (p_data.InteractBoxWidth / 2), hitbox.transform.position.y + (p_data.InteractBoxHeight / 2));
                break;

            case FacingDirection.left:
                hitbox.transform.localPosition = p_data.HitBoxPos;
                castPositionBotRight = new Vector2(hitbox.transform.position.x + (p_data.InteractBoxWidth / 2), hitbox.transform.position.y - (p_data.InteractBoxHeight / 2));
                castPositionTopLeft = new Vector2(hitbox.transform.position.x - (p_data.InteractBoxWidth / 2), hitbox.transform.position.y + (p_data.InteractBoxHeight / 2));
                break;
        }
    }

    public void CheckInteractCast()
    {
        Collider2D hit = Physics2D.OverlapArea(castPositionBotRight, castPositionTopLeft, p_data.whatIsInterObj, 1, 5);

        if (hit != null) {
            currentInterObj = hit.GetComponent<InterObj>();
            OnInteract();
        }
    }

    public void OnInteract()
    {
        switch (currentInterObj.Obj_Data.InterObjType)
        {
            case InterType.QuestEvent:
                VerifyQuestEventSwitch();
                break;

            case InterType.Storable:
                VerifyStorableSwitch();
                break;

            case InterType.Decoration:
                VerifyDecoSwitch();
                break;

            case InterType.Door:
                VerifyDoorSwitch();
                break;
        }

        //Send the current inter object to the dialogue handler
        SendObjToDialogueHandler(currentInterObj);
    }

    private void SendObjToDialogueHandler(InterObj objToSend)
    {
        if (objToSend != null) {
            player.UpdateBusyBool();
            GameObject.FindGameObjectWithTag("Dialogue Handler").SendMessage("InitializeDialogue", objToSend);

            SendObjToActionHandler();

            DumpInterObj();
        }
    }

    #region Verify Switch Cases
    private void VerifyQuestEventSwitch()
    {
        if (currentInterObj.Obj_Data.IsExhausted)
        {
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.ExhaustedDialogue);
            sendToActionHandler = false;
            return;
        }
        
        if (player.InvManager.FindItemInInv(currentInterObj.Obj_Data.RequiredItem))
        {
            //Found item in inventory, progress quest and display dialogue
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.QuestEventDialogue);

            //Send data to game manager, which will then send data to dialogue handler once quest is complete
            gm.SendMessage("ReceivedQuestRequirement", currentInterObj.gameObject);
            
            sendToActionHandler = true;
            currentInterObj.Obj_Data.IsExhausted = true;
        }
        else
        {
            //Item was not found, do not progress quest and display correct dialogue
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.MissingQuestItemDialogue);
            sendToActionHandler = false;
        }
    }

    private void SendObjToActionHandler()
    {
        if (sendToActionHandler)
        {
            gm.SendMessage("SendToActionHandler", currentInterObj);
        }
    }

    private void VerifyStorableSwitch()
    {
        if (!currentInterObj.Obj_Data.IsExhausted && player.InvManager.AddItemToInv(currentInterObj.gameObject))
        {
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.AddedToInventoryDialogue);

            if (currentInterObj.Obj_Data.ShouldBeDisabled)
            {
                currentInterObj.gameObject.SetActive(false);
            }

            sendToActionHandler = true;
            currentInterObj.Obj_Data.IsExhausted = true;
        }
        else
        {
            if (currentInterObj.Obj_Data.IsExhausted)
            {
                currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.ExhaustedDialogue);
            }
            else
            {
                currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.InventoryFullDialogue);
            }
            sendToActionHandler = false;
        }
    }

    private void VerifyDecoSwitch()
    {
        if (!currentInterObj.Obj_Data.IsExhausted)
        {
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.DecoDialogue);
            currentInterObj.Obj_Data.IsExhausted = true;
        }
        else
        {
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.ExhaustedDialogue);
        }
    }

    private void VerifyDoorSwitch()
    {
        //Is this door unlocked? 
        if (currentInterObj.Obj_Data.IsOpen)
        {
            gm.DoorHandler.DoorInteraction(currentInterObj);

            //Dump inter object to prevent any strange dialogue interactions
            DumpInterObj();
        }
        else
        {
            //Display door locked dialogue
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.DoorLockedDialogue);
        }
    }
    #endregion

    private void DumpInterObj() => currentInterObj = null;
}
