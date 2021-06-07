using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    up,
    down,
    left,
    right
}

public class PlayerInteract
{
    private PlayerData p_data;
    private PlayerInputHandler inputHandler;
    private Player player;

    private FacingDirection direction;

    private InterObj interObj;

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
        //Change to player facing direction once animations are added
        //Or, incorporate "update cast position" into animation updator
        //Put enum direction into player scripts, have other scripts pull and alter that direction
        //Use direction property
        if (inputHandler.NormInputX == 1) {
            direction = FacingDirection.right;
        } else if (inputHandler.NormInputX == -1) {
            direction = FacingDirection.left;
        } else if (inputHandler.NormInputY == 1) {
            direction = FacingDirection.up;
        } else if (inputHandler.NormInputY == -1) {
            direction = FacingDirection.down;
        }

        //Depending on direction of player, update where the hit casts
        switch (direction)
        {
            case FacingDirection.right:
                interactCheck.transform.localPosition = new Vector3(p_data.InteractTestCheckDistance, 0, 0);
                Debug.Log("Check Set to Right");
                break;

            case FacingDirection.left:
                interactCheck.transform.localPosition = new Vector3(-p_data.InteractTestCheckDistance, 0, 0);
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

        if (hit != null)
        {
            interObj = hit.GetComponent<InterObj>();
        }
    }

    public void OnInteract()
    {
        if (interObj != null)
        {
            Debug.Log(interObj);
            //What Object is this? What do we need to do with it?
            CheckInteractCast().obj_Data.
            GameObject.FindGameObjectWithTag("Dialogue Handler").SendMessage("");
        } else {
            return;
        }
    }
}
