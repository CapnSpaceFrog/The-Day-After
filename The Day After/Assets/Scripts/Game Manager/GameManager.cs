using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTimer Timer { get; private set; }
    public PostDialogueHandler PostDialogue { get; private set; }
    public Player PlayerRef { get; private set; }

    #region Quest Components
    public QuestManager QuestManager { get; private set; }

    [SerializeField]
    private QuestData[] questData = new QuestData[5];
    #endregion


    private void Awake()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Timer = new GameTimer();

        QuestManager = new QuestManager(questData);
        PostDialogue = new PostDialogueHandler(PlayerRef);
    }

    private void Update()
    {
        QuestManager.IsQuestComplete();

        if (Timer.HasTimeExpired())
        {
            //Trigger Game Over Sequence Regardless of players current interaction
        }
    }

    public void ReceivedQuestRequirement(GameObject req)
    {
        QuestManager.ReceivedRequirement(req);
    }

    public void ReceiveObjPostDialogue(InterObj objToUpdate)
    {
        PostDialogue.CheckIfShouldUpdate(objToUpdate);
    }
}
