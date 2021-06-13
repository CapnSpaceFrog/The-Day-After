using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    private GameManager gm;

    #region Quests
    private Quest QuestOne;
    private Quest QuestTwo;
    private Quest QuestThree;
    private Quest QuestFour;
    private Quest QuestFive;
    #endregion

    private Quest[] quests;
    private QuestData[] questData;

    public Quest CurrentQuest { get; private set; }
    public Quest FinalQuest { get; private set; }
    private int currentQuestInt;

    //Currently no information being passed into the quests to know if they are completed or not
    public QuestManager(QuestData[] questData, GameManager gm)
    {
        QuestOne = new Quest(questData[0].QuestRequirements);
        QuestTwo = new Quest(questData[1].QuestRequirements);
        QuestThree = new Quest(questData[2].QuestRequirements);
        //QuestFour = new Quest(questData[3].QuestRequirements);
        //QuestFive = new Quest(questData[4].QuestRequirements);

        
        quests = new Quest[] { QuestOne, QuestTwo, QuestThree };
        FinalQuest = quests[quests.Length - 1];

        this.questData = questData;
        this.gm = gm;
        CurrentQuest = quests[0];
        currentQuestInt = 0;
    }

    public void IsQuestComplete()
    {
        if (CurrentQuest.QuestComplete)
        {
            if (CurrentQuest == FinalQuest)
            {
                gm.GameCompleted();
                return;
            }
            UpdateActiveQuest();
        }
    }
    
    public void UpdateActiveQuest()
    {
        //Tack on End of Quest Dialogue 
        GameObject.FindGameObjectWithTag("Dialogue Handler").SendMessage("QuestDialogueAdd", questData[currentQuestInt].QuestCompleteDialogue);
        
        //open doors that we need too
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Door");
        for (int i = 0; i < questData[currentQuestInt].DoorsToUnlock.Length; i++)
        {
            foreach (GameObject door in temp)
            {
                if (questData[currentQuestInt].DoorsToUnlock[i] == door.name)
                {
                    gm.DoorHandler.UnlockDoor(door);
                }
            }
        }

        //Change current quest and update current int
        ChangeActiveQuest(quests[currentQuestInt + 1]);
        currentQuestInt++;
    }

    public void ChangeActiveQuest(Quest newQuest)
    {
        CurrentQuest = newQuest;
    }

    public void ReceivedRequirement(GameObject req)
    {
        CurrentQuest.CheckIfRequirementMatch(req);
    }
}
