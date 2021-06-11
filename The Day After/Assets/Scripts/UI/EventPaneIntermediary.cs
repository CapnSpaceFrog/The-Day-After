using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPaneIntermediary : MonoBehaviour
{
    public void ClearTextDisplay()
    {
        GameObject.FindGameObjectWithTag("UI Handler").SendMessage("ClearText");
    }
}
