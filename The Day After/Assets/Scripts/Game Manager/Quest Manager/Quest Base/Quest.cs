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
        foreach (GameObject req in requirements)
        {
            if (req.name == requiredObj.name)
            {
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
                return;
            }
        }
        QuestComplete = true;
    }
}
