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

    [Header("Storable Dialogue")]
    public string[] StoredItem;

    [Header("Deco Dialogue")]
    public string[] DecoDialogue;

    [Header("Misc Dialogue")]
    public string[] InventoryFullDialogue;
    public string[] AddedToInventoryDialogue;

    [Header("Quest Event")]
    public GameObject RequiredItem;

    [Header("Anim Variables")]
    public Animator InterObjAnim;

    [Header("Door Variables")]
    public string WhereToMove;
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
    both,
    none
}

public enum UpdateTime
{
    before,
    after,
    both
}
