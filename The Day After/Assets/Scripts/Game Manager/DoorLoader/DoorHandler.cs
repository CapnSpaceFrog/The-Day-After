using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorHandler
{
    private Player playerRef;
    private GameManager gm;

    private Animator transitionAnim;
    
    public DoorHandler(Player playerRef, GameManager gm)
    {
        this.playerRef = playerRef;
        this.gm = gm;

        transitionAnim = GameObject.FindGameObjectWithTag("Room Transition Panel").GetComponent<Animator>();
    }

    public void DoorInteraction(InterObj door)
    {
        gm.StartCoroutine(ChangeRoom(door));
    }

    private IEnumerator ChangeRoom(InterObj door)
    {
        playerRef.UpdateBusyBool();
        door.ChangeAnimationState("DOOR_OPEN");
        transitionAnim.Play("ROOM_FADEIN");

        yield return new WaitForSeconds(transitionAnim.GetCurrentAnimatorStateInfo(0).length);
        door.ChangeAnimationState("DOOR_CLOSED");
        MovePlayerPos(FindTargetPos(door.Obj_Data.TargetDoor));
        transitionAnim.Play("ROOM_FADEOUT");
        yield return new WaitForSeconds(transitionAnim.GetCurrentAnimatorStateInfo(0).length);
        playerRef.UpdateBusyBool();
    }

    private Vector3 FindTargetPos(string targetDoor)
    {
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            if (door.name == targetDoor)
            {
                return door.transform.Find("Player Spawn Pos").transform.position;
            }
        }
        return new Vector3(-500, 0, 0);
    }

    private void MovePlayerPos(Vector3 posToMove)
    {
        playerRef.transform.position = posToMove;
    }

    public void UnlockDoor(GameObject doorToUnlock)
    {
        doorToUnlock.GetComponent<InterObj>().Obj_Data.IsOpen = true;
    }
}
