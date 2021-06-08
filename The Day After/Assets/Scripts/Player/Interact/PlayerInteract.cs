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

    private InterObj interObj;

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
                Debug.Log("Check Set to Right");
                break;

            case FacingDirection.left:
                interactCheck.transform.localPosition = new Vector3(p_data.InteractTestCheckDistance, 0, 0);
                Debug.Log("Check Set to left");
                break;

            //Reminder to double check this distance once we are play testing inside the environment
            case FacingDirection.up:
                interactCheck.transform.localPosition = new Vector3(0, p_data.InteractTestCheckDistance, 0);
                Debug.Log("Check Set to up");
                break;

            case FacingDirection.down:
                interactCheck.transform.localPosition = new Vector3(0, -p_data.InteractTestCheckDistance, 0);
                Debug.Log("Check Set to down");
                break;
        }
    }

    public void CheckInteractCast()
    {
        Collider2D hit = Physics2D.OverlapPoint(interactCheck.transform.position, p_data.whatIsInterObj);

        if (hit != null) {
            interObj = hit.GetComponent<InterObj>();
            OnInteract();
        }
    }

    public void OnInteract()
    {
        switch (interObj.Obj_Data.InterType)
        {
            case InterType.QuestEvent:
                if (player.InvManager.FindItemInInv(interObj.Obj_Data.RequiredItem))
                {
                    Debug.Log("Item Found, Quest Progressed");
                    //Found item in inventory, progress quest and display dialogue
                    //Reminder to send dialogue handler inter object
                }
                else
                {
                    Debug.Log("Did not find item, quest not progressed");
                    //Item was not found, do not progress quest and display correct dialogue
                }
                break;

            case InterType.Storable:
                if (player.InvManager.AddItemToInv(interObj.gameObject))
                {
                    Debug.Log("Added to inventory");
                    interObj.gameObject.SetActive(false);
                    //Item successfully added, do not display inventory is full
                }
                else
                {
                    Debug.Log("Did not store item, inventory full");
                    //Inventory was full, display inventory full dialogue
                }
                break;

            case InterType.Decoration:
                Debug.Log("Displayed Dialogue");
                //This object simply sends some dialogue to the Dialogue Handler
                break;
        }

        DumpInterObj();
    }

    private void DumpInterObj() => interObj = null;
}
