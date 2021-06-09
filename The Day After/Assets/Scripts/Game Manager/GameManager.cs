using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTimer Timer { get; private set; }
    public DialogueActionHandler DialogueActionHandler { get; private set; }
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
        DialogueActionHandler = new DialogueActionHandler(PlayerRef, this);
    }

    private void Update()
    {
        Debug.Log(DialogueActionHandler.dialogueFinished);
        QuestManager.IsQuestComplete();

        if (Timer.HasTimeExpired())
        {
            //Trigger Game Over Sequence Regardless of players current interaction
        }
    }

    #region Quest Manager Methods
    public void ReceivedQuestRequirement(GameObject req)
    {
        QuestManager.ReceivedRequirement(req);
    }
    #endregion

    #region Dialogue Action Methods
    public void SendToActionHandler(InterObj objToUpdate)
    {
        Debug.Log("Received Inter Obj in Game Manager");
        DialogueActionHandler.CheckWhenToUpdate(objToUpdate);
    }

    public void UpdateDialogueFinish(bool isDialogueFinished)
    {
        DialogueActionHandler.UpdateDialogueFinish(isDialogueFinished);
    }
    #endregion
}
