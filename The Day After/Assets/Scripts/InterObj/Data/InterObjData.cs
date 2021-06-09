using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newInterObjData", menuName = "Data/InterObj Data/Base Data")]
public class InterObjData : ScriptableObject
{
    [Header("InterObj Type")]
    public InterType InterType;

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
}

public enum InterType
{
    Decoration,
    Storable,
    QuestEvent
}
