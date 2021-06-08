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

    public void Update()
    {
        AnimManager.UpdateAnims();
    }

    private void FixedUpdate()
    {
        Movement.SetVelocity(P_Data.Movespeed);

        if (InputHandler.InteractInput)
        {
            InputHandler.UseInteractInput();
            Interact.CheckInteractCast();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Interact.interactCheck.transform.position, P_Data.GizmoDrawRadius);
    }
}
