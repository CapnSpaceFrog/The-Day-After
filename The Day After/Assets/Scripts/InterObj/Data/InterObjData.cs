using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newInterObjData", menuName = "Data/InterObj Data/Base Data")]
public class InterObjData : ScriptableObject
{
    [Header("InterObj Type")]
    public InterType InterType;

    [Header("Dialogue")]
    public string DisplayName;
    public string[] Dialogue;
    public Sprite[] DialogueSprite;

    [Header("Quest Event")]
    public GameObject RequiredItem;
}

public enum InterType
{
    Decoration,
    Storable,
    QuestEvent
}
