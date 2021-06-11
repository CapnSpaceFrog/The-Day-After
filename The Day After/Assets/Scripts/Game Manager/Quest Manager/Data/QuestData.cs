using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newQuestData", menuName = "Data/Quest Data/Base Data")]
public class QuestData : ScriptableObject
{
    [Header("Quest Requirments")]
    public GameObject[] QuestRequirements;
    public string[] DoorToUnlock;

    [Header("Completed Quest Dialogue")]
    public string[] QuestCompleteDialogue;
}
