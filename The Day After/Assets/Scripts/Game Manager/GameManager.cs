using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTimer Timer { get; private set; }

    #region Quest Components
    public QuestManager QuestManager { get; private set; }
    [SerializeField]
    private QuestData questOneData;
    [SerializeField]
    private QuestData questTwoData;
    [SerializeField]
    private QuestData questThreeData;
    [SerializeField]
    private QuestData questFourData;
    [SerializeField]
    private QuestData questFiveData;
    #endregion


    private void Awake()
    {
        Timer = new GameTimer();

        QuestData[] questData = new QuestData[] { questOneData, questTwoData, questThreeData, questFourData, questFiveData };

        QuestManager = new QuestManager(questData);
    }

    private void Update()
    {
        Debug.Log(QuestManager.CurrentQuest);
        QuestManager.IsQuestComplete();

        if (Timer.HasTimeExpired())
        {
            //Trigger Game Over Sequence Regardless of players current interaction
        }
    }

    public void ReceivedRequirement(GameObject req)
    {
        QuestManager.ReceivedRequirement(req);
    }
}
