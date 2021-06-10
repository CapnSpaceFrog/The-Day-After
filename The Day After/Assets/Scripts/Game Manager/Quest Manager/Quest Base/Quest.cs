using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private GameObject[] requirements;

    private bool[] completedReq;

    public bool QuestComplete { get; private set; }
    
    public Quest(GameObject[] requirements)
    {
        this.requirements = requirements;

        completedReq = new bool[requirements.Length];
    }

    public void CheckIfRequirementMatch(GameObject requiredObj)
    {
        Debug.Log("Checking Requirement Match");
        foreach (GameObject req in requirements)
        {
            if (req.name == requiredObj.name)
            {
                Debug.Log("Received Requirement Matches");
                CompletedRequirement();
            }
        }
    }

    private void CompletedRequirement()
    {
        for (int i = 0; i < completedReq.Length; i++)
        {
            if (completedReq[i] == false)
            {
                completedReq[i] = true;
                Debug.Log("Updated Completed Array");
                break;
            }
        }
        CheckIfQuestComplete();
    }

    private void CheckIfQuestComplete()
    {
        for (int i = 0; i < completedReq.Length; i++)
        {
            if (completedReq[i] == false)
            {
                Debug.Log("Failsafe Was Triggered");
                return;
            }
        }
        QuestComplete = true;
        Debug.Log("Quest Completed in Quest Class");
    }
}
