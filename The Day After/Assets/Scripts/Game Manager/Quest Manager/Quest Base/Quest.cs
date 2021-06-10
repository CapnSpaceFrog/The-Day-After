using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private GameObject[] requirements;

    private GameObject[] completedReq;

    public bool QuestComplete { get; private set; }
    
    public Quest(GameObject[] requirements)
    {
        this.requirements = requirements;

        completedReq = new GameObject[requirements.Length];
    }

    public void CheckIfRequirementMatch(GameObject requiredObj)
    {
        Debug.Log("Checking Requirement Match");
        foreach (GameObject req in requirements)
        {
            if (req.name == requiredObj.name)
            {
                Debug.Log("Received Requirement Matches");
                CompletedRequirement(requiredObj);
            }
        }
    }

    private void CompletedRequirement(GameObject requiredObj)
    {
        for (int i = 0; i < completedReq.Length; i++)
        {
            if (completedReq[i] == null)
            {
                completedReq[i] = requiredObj;
                break;
            }
        }
        CheckIfQuestComplete();
    }

    private void CheckIfQuestComplete()
    {
        for (int i = 0; i < completedReq.Length; i++)
        {
            if (completedReq[i] == null)
            {
                Debug.Log("Failsafe Was Triggered");
                return;
            }
        }
        QuestComplete = true;
        Debug.Log("Quest Completed in Quest Class");
    }
}
