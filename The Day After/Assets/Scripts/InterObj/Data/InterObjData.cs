using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newInterObjData", menuName = "Data/InterObj Data/Base Data")]
public class InterObjData : ScriptableObject
{
    [Header("Enum Variables")]
    public InterType InterObjType;
    public UpdateType WhatToUpdate;
    public UpdateTime WhenToUpdate;

    [Header("Dialogue")]
    public List<string> DisplayDialogue;
    public Sprite[] DisplaySprite;

    [Header("QuestEvent Dialogue")]
    public string[] QuestEventDialogue;
    public string[] MissingQuestItemDialogue;
    public string[] QuestExhaustedDialogue;

    [Header("Stored Dialogue")]
    public string[] InventoryFullDialogue;
    public string[] AddedToInventoryDialogue;
    public Sprite InventoryIcon;

    [Header("Deco Dialogue")]
    public string[] DecoDialogue;

    [Header("Quest Event")]
    public GameObject RequiredItem;
    public bool IsExhausted;

    [Header("Anim Variables")]
    public string[] BeforeAnimsToPlay;
    public string[] AfterAnimsToPlay;
    [HideInInspector]
    public Animator InterObjAnim;

    [Header("Door Variables")]
    public bool IsOpen;
    public string TargetDoor;
    public string[] DoorLockedDialogue;
}

public enum InterType
{
    Decoration,
    Storable,
    QuestEvent,
    Door
}

public enum UpdateType
{
    player,
    itself,
    both
}

public enum UpdateTime
{
    before,
    after,
    both,
    none
}
