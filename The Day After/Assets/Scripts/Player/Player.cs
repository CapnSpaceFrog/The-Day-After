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
    #endregion

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();

        Movement = new PlayerMovement(RB, InputHandler);
        Interact = new PlayerInteract(this, P_Data, InputHandler);
    }

    private void Update()
    {
        Movement.SetVelocity(P_Data.Movespeed);

        Interact.UpdateInteractCastPosition();

        if (InputHandler.InteractInput)
        {
            InputHandler.UseInteractInput();
            Interact.OnInteract();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Interact.interactCheck.transform.position, P_Data.GizmoDrawRadius);
    }
}
