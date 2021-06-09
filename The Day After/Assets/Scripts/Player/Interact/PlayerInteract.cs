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
    private PlayerInputHandler inputHandler;
    private Player player;

    private InterObj currentInterObj;

    private FacingDirection direction;

    public FacingDirection Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    //Reminder to set to private when done testing
    public GameObject interactCheck;

    public PlayerInteract(Player player, PlayerData p_data, PlayerInputHandler inputHandler)
    {
        this.p_data = p_data;
        this.inputHandler = inputHandler;
        this.player = player;

        CreateInteractCheck();
    }

    private void CreateInteractCheck()
    {
        interactCheck = new GameObject("Interact Check");
        interactCheck.transform.parent = player.transform;
        interactCheck.transform.localPosition = Vector3.zero;
    }

    public void UpdateInteractCastPosition()
    {
        //Depending on direction of player, update where the hit casts
        switch (direction)
        {
            case FacingDirection.right:
                interactCheck.transform.localPosition = new Vector3(p_data.InteractTestCheckDistance, 0, 0);
                break;

            case FacingDirection.left:
                interactCheck.transform.localPosition = new Vector3(p_data.InteractTestCheckDistance, 0, 0);
                break;

            //Reminder to double check this distance once we are play testing inside the environment
            case FacingDirection.up:
                interactCheck.transform.localPosition = new Vector3(0, p_data.InteractTestCheckDistance, 0);
                break;

            case FacingDirection.down:
                interactCheck.transform.localPosition = new Vector3(0, -p_data.InteractTestCheckDistance, 0);
                break;
        }
    }

    public void CheckInteractCast()
    {
        Collider2D hit = Physics2D.OverlapPoint(interactCheck.transform.position, p_data.whatIsInterObj);

        if (hit != null) {
            currentInterObj = hit.GetComponent<InterObj>();
            OnInteract();
        }
    }

    public void OnInteract()
    {
        switch (currentInterObj.Obj_Data.InterType)
        {
            case InterType.QuestEvent:
                if (player.InvManager.FindItemInInv(currentInterObj.Obj_Data.RequiredItem))
                {
                    //Found item in inventory, progress quest and display dialogue
                    currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.QuestEventDialogue);
                    SendOnjToDialogueHandler(currentInterObj);

                    //Ask the game manager what we're supposed to do since this was progressed.
                }
                else
                {
                    //Item was not found, do not progress quest and display correct dialogue
                    currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.MissingQuestItemDialogue);
                    SendOnjToDialogueHandler(currentInterObj);
                }
                break;

            case InterType.Storable:
                if (player.InvManager.AddItemToInv(currentInterObj.gameObject))
                {
                    //Item successfully added, do not display inventory is full
                    currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.AddedToInventoryDialogue);
                    SendOnjToDialogueHandler(currentInterObj);
                    currentInterObj.gameObject.SetActive(false);
                }
                else
                {
                    //Inventory was full, display inventory full dialogue
                    currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.InventoryFullDialogue);
                    SendOnjToDialogueHandler(currentInterObj);
                }
                break;

            case InterType.Decoration:
                //This object simply sends some dialogue to the Dialogue Handler
                currentInterObj.OverrideDisplayString(currentInterObj.Obj_Data.DecoDialogue);
                SendOnjToDialogueHandler(currentInterObj);
                
                break;
        }

        DumpInterObj();
    }

    private void SendOnjToDialogueHandler(InterObj objToSend)
    {
        GameObject.FindGameObjectWithTag("Dialogue Handler").SendMessage("InitializeDialogue", objToSend);
    }

    private void DumpInterObj() => currentInterObj = null;
}
