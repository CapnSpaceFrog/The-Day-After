using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    right,
    left,
    up,
    down
}
public class PlayerInteract
{
    private PlayerData p_data;
    private Player player;
    private GameManager gm;

    private InterObj currentInterObj;

    private bool canSendQuest;

    private FacingDirection direction;
    
    public Vector2 rayDirection;
    public float InteractCheckDistance;

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
    }

    public void UpdateInteractCastPosition()
    {
        //Depending on direction of player, update where the hit casts
        //Reminder to double check this distance once we are play testing inside the environment
        switch (direction)
        {
            case FacingDirection.right:
                rayDirection = Vector2.right;
                InteractCheckDistance = p_data.InteractCheckDistanceHorizontal;
                break;

            case FacingDirection.left:
                rayDirection = Vector2.left;
                InteractCheckDistance = p_data.InteractCheckDistanceHorizontal;
                break;

            case FacingDirection.up:
                rayDirection = Vector2.up;
                InteractCheckDistance = p_data.InteractCheckDistanceUp;
                break;

            case FacingDirection.down:
                rayDirection = Vector2.down;
                InteractCheckDistance = p_data.InteractCheckDistanceDown;
                break;
        }
    }

    public void CheckInteractCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, rayDirection, InteractCheckDistance, p_data.whatIsInterObj);
        Collider2D obj = hit.collider;

        if (obj != null) {
            currentInterObj = obj.GetComponent<InterObj>();
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
        if (player.InvManager.FindItemInInv(currentInterObj.Obj_Data.RequiredItem))
        {
            //Found item in inventory, progress quest and display dialogue
            currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.QuestEventDialogue);

            //Send data to game manager, which will then send data to dialogue handler once quest is complete
            gm.SendMessage("ReceivedQuestRequirement", currentInterObj.gameObject);

            canSendQuest = true;
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
