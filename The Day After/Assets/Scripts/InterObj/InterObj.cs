using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObj : MonoBehaviour
{
    public InterObjData Obj_Data;

    private string currentState;

    private void Awake()
    {
        Obj_Data.InterObjAnim = GetComponent<Animator>();
        Obj_Data.IsOpen = false;
        Obj_Data.IsExhausted = false;
    }

    //Sets Display String to whatever string array overide we want
    public void OverrideDisplayString(string[] stringOverride, Sprite[] spriteOverride)
    {
        Obj_Data.DisplayDialogue = new List<string>(new string[stringOverride.Length]);
        Obj_Data.DisplaySprite = new List<Sprite>(new Sprite[spriteOverride.Length]);
        for (int i = 0; i < stringOverride.Length; i++)
        {
            Obj_Data.DisplayDialogue[i] = stringOverride[i];
            Obj_Data.DisplaySprite[i] = spriteOverride[i];
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        Obj_Data.InterObjAnim.Play(newState);

        currentState = newState;
    }
}
