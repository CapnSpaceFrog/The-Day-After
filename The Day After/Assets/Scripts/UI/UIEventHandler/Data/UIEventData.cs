using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUIEventData", menuName = "Data/UI Event Data/Base Data")]
public class UIEventData : ScriptableObject
{
    public string[] DialogueToDisplay;
    public Sprite[] SpritesToDisplay;
    public bool IsPreEvent;

    [Header("Post Game Dialogue")]
    public string[] FinishedInTimeDialogue;
    public Sprite[] FinishedInTimeSpriteDisplay;
}
