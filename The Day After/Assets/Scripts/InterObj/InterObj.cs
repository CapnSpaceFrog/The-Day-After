using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObj : MonoBehaviour
{
    public InterObjData Obj_Data;

    private void Awake()
    {
        Obj_Data.InterObjAnim = GetComponent<Animator>();
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
}
