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

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int) (RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int) (RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractInput = true;
        }
    }

    public void UseInteractInput() => InteractInput = false;

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseInput = true;
        }

        if (context.canceled)
        {
            PauseInput = false;
        }
    }
}
