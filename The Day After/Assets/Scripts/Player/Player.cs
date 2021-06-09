using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Component Variables
    public PlayerInputHandler InputHandler { get; private set; }
    [HideInInspector]
    public Rigidbody2D RB;

    public PlayerData P_Data;

    public PlayerMovement Movement { get; private set; }
    public PlayerInteract Interact { get; private set; }
    public PlayerInventory InvManager { get; private set; }
    public PlayerAnimator AnimManager { get; private set; }
    #endregion

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();

        Movement = new PlayerMovement(RB, InputHandler);
        Interact = new PlayerInteract(this, P_Data, InputHandler);
        InvManager = new PlayerInventory(P_Data);
        AnimManager = new PlayerAnimator(this, P_Data, InputHandler);
    }

    private void Start()
    {
        P_Data.InDialogue = false;
    }

    public void Update()
    {
        if (!P_Data.InDialogue)
        {
            AnimManager.UpdateAnims();
            AnimManager.GotDressed();
        }
    }

    private void FixedUpdate()
    {
        if (!P_Data.InDialogue)
        {
            Movement.SetVelocity(P_Data.Movespeed);
        } else
        {
            Movement.SetVelocity(0);
        }
    }

    #region Other Functions
    public void UpdateDialogueBool() 
    {
        if (!P_Data.InDialogue)
        {
            P_Data.InDialogue = true;
            AnimManager.ChangeAnimationState(AnimManager.PLAYER_IDLE);
        }
        else
        {
            P_Data.InDialogue = false;
        } 
    }
    #endregion
}
