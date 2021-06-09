using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObj : MonoBehaviour
{
    public InterObjData Obj_Data;

    //Handle which dialogue string to display based on information fed from PLayer Interact Script

    //Sets Display string to inventory full string

    public void OverrideDisplayString(string[] stringOverride)
    {
        Obj_Data.DisplayDialogue = new List<string>(new string[stringOverride.Length]);
        for (int i = 0; i < stringOverride.Length; i++)
        {
            Obj_Data.DisplayDialogue[i] = stringOverride[i];
        }
    }
}
