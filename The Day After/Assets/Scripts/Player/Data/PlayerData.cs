using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Variables")]
    public float Movespeed;

    [Header("Interact Variables")]
    public LayerMask whatIsInterObj;
    public float InteractTestCheckDistance;

    [Header("Inventory Variables")]
    public int InventorySize;

    [Header("Test Variables")]
    public float GizmoDrawRadius;

    [Header("Dialogue Variables")]
    public bool InDialogue;
}
