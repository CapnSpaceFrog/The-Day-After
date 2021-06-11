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

    private bool canSendQuest;

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
        Collider2D hit = Physics2D.OverlapArea(castPositionBotRight, castPositionTopLeft, p_data.whatIsInterObj);

        if (hit != null) {
            currentInterObj = hit.GetComponent<InterObj>();
            OnInteract();
        }
    }

    public void OnInteract()
    {
        Debug.Log("OnInteract");
        switch (currentInterObj.Obj_Data.InterObjType)
        {
            case InterType.QuestEvent:
                VerifyQuestEventSwitch();
                break;

            case InterType.Storable:
                VerifyStorableSwitch();
                break;

            case InterType.Decoration:
                //This object simply sends some dialogue to the Dialogue Handler
                currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.DecoDialogue);
                Debug.Log("Deco Switch");
                break;

            case InterType.Door:
                Debug.Log("Door Switch");
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
        Debug.Log("QuestEvent Switch");
        if (currentInterObj.Obj_Data.IsExhausted)
        {
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.QuestExhaustedDialogue);
            canSendQuest = false;
            return;
        }
        
        if (player.InvManager.FindItemInInv(currentInterObj.Obj_Data.RequiredItem))
        {
            //Found item in inventory, progress quest and display dialogue
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.QuestEventDialogue);

            //Send data to game manager, which will then send data to dialogue handler once quest is complete
            gm.SendMessage("ReceivedQuestRequirement", currentInterObj.gameObject);
            
            canSendQuest = true;
            currentInterObj.Obj_Data.IsExhausted = true;
        }
        else
        {
            //Item was not found, do not progress quest and display correct dialogue
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.MissingQuestItemDialogue);
            canSendQuest = false;
        }
    }

    private void SendObjToActionHandler()
    {
        if (canSendQuest)
        {
            gm.SendMessage("SendToActionHandler", currentInterObj);
        }
    }

    private void VerifyStorableSwitch()
    {
        Debug.Log("Storable Switch");
        if (player.InvManager.AddItemToInv(currentInterObj.gameObject))
        {
            //Item successfully added, do not display inventory is full
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.AddedToInventoryDialogue);
            currentInterObj.gameObject.SetActive(false);
        }
        else
        {
            //Inventory was full, display inventory full dialogue
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.InventoryFullDialogue);
        }
    }
    #endregion

    private void DumpInterObj() => currentInterObj = null;
}
