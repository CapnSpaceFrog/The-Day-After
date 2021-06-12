using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEvenIntermediary : MonoBehaviour
{
    public Animator anim;
    public UIEventHandler UIHandler;

    public void OnAnimFinish()
    {
        anim.Play("PLAYER_IDLE");
    }

    public void OnFinalAnimFinish()
    {
        UIHandler.momAnim.Play("MOM_HUG");
        anim.Play("PLAYER_EMPTY");
    }
}
