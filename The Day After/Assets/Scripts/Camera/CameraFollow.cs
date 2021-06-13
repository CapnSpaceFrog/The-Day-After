using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    public float xFollowOffset;
    public float yFollowOffset;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + xFollowOffset, player.transform.position.y + yFollowOffset, -1);
    }
}
