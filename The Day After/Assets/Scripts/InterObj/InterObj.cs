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
        if (Obj_Data.HasPJSprite)
        {
            Obj_Data.DisplaySprite = Resources.Load<Sprite>("Sprites/PJSprite");
        } else
        {
            Obj_Data.DisplaySprite = Resources.Load<Sprite>("Sprites/ClothedSprite");
        }
    }

    //Sets Display String to whatever string array overide we want
    public void OverrideDisplayString(string[] stringOverride)
    {
        Obj_Data.DisplayDialogue = new List<string>(new string[stringOverride.Length]);
        for (int i = 0; i < stringOverride.Length; i++)
        {
            Obj_Data.DisplayDialogue[i] = stringOverride[i];
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        Obj_Data.InterObjAnim.Play(newState);

        currentState = newState;
    }
}
