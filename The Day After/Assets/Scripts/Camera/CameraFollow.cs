using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    public Vector3 followOffset;

    private void LateUpdate()
    {
        transform.position = player.position + followOffset;
    }
}
