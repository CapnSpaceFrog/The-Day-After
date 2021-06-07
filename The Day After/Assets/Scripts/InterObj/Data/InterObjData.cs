using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newInterObjData", menuName = "Data/InterObj Data/Base Data")]
public class InterObjData : ScriptableObject
{
    [Header("InterObj Type")]
    public bool IsStorable;

    [Header("Dialogue")]
    public string DisplayName;
    public string[] Dialogue;
    public Sprite DisplaySprite;
}
