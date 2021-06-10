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
            if (!completedReq[i])
            {
                Debug.Log("Updated Completed Array");
                completedReq[i] = true;
                break;
            }
        }

        CheckIfQuestComplete();
    }

    private void CheckIfQuestComplete()
    {
        foreach (bool req in completedReq)
        {
            if (!req)
            {
                break;
            }
            else
            {
                Debug.Log("Quest Completed in Quest Class");
                QuestComplete = true;
            }
        }
    }
}