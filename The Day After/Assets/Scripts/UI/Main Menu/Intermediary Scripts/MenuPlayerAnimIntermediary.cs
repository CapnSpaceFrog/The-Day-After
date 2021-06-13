using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerAnimIntermediary : MonoBehaviour
{
    public void PlayerInPosition()
    {
        GameObject.FindGameObjectWithTag("Main Menu Handler").SendMessage("PlayerInPosition");
        GetComponent<Animator>().Play("PLAYER_SIT");
    }
}
