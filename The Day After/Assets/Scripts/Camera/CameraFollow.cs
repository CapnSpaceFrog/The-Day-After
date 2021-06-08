using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void LateUpdate()
    {
        transform.position = player.position;
    }
}
