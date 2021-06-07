using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private Rigidbody2D playerRB;
    private PlayerInputHandler inputHandler;

    private Vector2 workspace;

    public PlayerMovement (Rigidbody2D playerRB, PlayerInputHandler inputHandler)
    {
        this.playerRB = playerRB;
        this.inputHandler = inputHandler;
    }

    public void SetVelocity(float velocity)
    {
        workspace.Set(inputHandler.NormInputX * velocity, inputHandler.NormInputY * velocity);
        playerRB.velocity = workspace;
    }
}
