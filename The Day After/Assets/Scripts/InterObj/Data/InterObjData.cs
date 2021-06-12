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
    public List<Sprite> DisplaySprite;
    public string[] ExhaustedDialogue;
    public Sprite[] ExhaustedSprites;

    [Header("QuestEvent Dialogue")]
    public string[] QuestEventDialogue;
    public Sprite[] QuestEventSprites;
    public string[] MissingQuestItemDialogue;
    public Sprite[] MissingQuestItemSprites;

    [Header("Stored Dialogue")]
    public bool ShouldBeDisabled;
    public string[] InventoryFullDialogue;
    public Sprite[] InventoryFullSprites;
    public string[] AddedToInventoryDialogue;
    public Sprite[] AddedToInvSprites;
    public Sprite InventoryIcon;

    [Header("Deco Dialogue")]
    public string[] DecoDialogue;
    public Sprite[] DecoSprites;

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
    public Sprite[] DoorLockedSprites;
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
