using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator
{
    private PlayerData p_data;
    private Player player;
    private PlayerInputHandler inputHandler;

    private Animator playerAnim;
    private string currentState;

    private int lookDirection;
    private bool canFlip;

    #region Animations
    public string PLAYER_IDLE = "PLAYER_IDLE";
    public string PLAYER_MOVE = "PLAYER_MOVE";
    #endregion

    public PlayerAnimator(Player player, PlayerData p_data, PlayerInputHandler inputHandler)
    {
        this.player = player;
        this.p_data = p_data;
        this.inputHandler = inputHandler;

        playerAnim = player.GetComponent<Animator>();

        lookDirection = 1;
    }

    public void UpdateAnims()
    {
        if (inputHandler.NormInputX != 0 || inputHandler.NormInputY != 0)
        {
            ChangeAnimationState(PLAYER_MOVE);
        } else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        if (inputHandler.NormInputX == 1)
        {
            player.Interact.Direction = FacingDirection.right;
        }
        else if (inputHandler.NormInputX == -1)
        {
            player.Interact.Direction = FacingDirection.left;
        }
        else if (inputHandler.NormInputY == 1)
        {
            player.Interact.Direction = FacingDirection.up;
        }
        else if (inputHandler.NormInputY == -1)
        {
            player.Interact.Direction = FacingDirection.down;
        }

        player.Interact.UpdateInteractCastPosition();
        CheckIfFlip();
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        playerAnim.Play(newState);

        currentState = newState;
    }

    public void CheckIfFlip()
    {
        if (inputHandler.NormInputX != 0 && inputHandler.NormInputX != lookDirection)
        {
            player.transform.Rotate(0, 180, 0);
            lookDirection *= -1;
        }
    }
}
