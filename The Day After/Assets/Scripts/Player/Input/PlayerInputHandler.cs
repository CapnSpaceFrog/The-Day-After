using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Player player;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool InteractInput { get; private set; }
    public bool PauseInput { get; private set; }

    public void Start()
    {
        player = GetComponent<Player>();
    }

    public void FixedUpdate()
    {
        if (InteractInput)
        {
            OnInteractInput();
            UseInteractInput();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnInterInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractInput = true;
        }
    }

    private void OnInteractInput()
    {
        if (!player.P_Data.IsBusy) {
            player.Interact.CheckInteractCast();
            UseInteractInput();
        } else {
            UseInteractInput();
        }
    }

    private void UseInteractInput() => InteractInput = false;

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseInput = true;
        }
    }

    public void UsePauseInput() => PauseInput = false;
}
