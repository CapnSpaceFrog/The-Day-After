using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTimer Timer { get; private set; }
    public DialogueActionHandler DialogueActionHandler { get; private set; }
    public Player PlayerRef { get; private set; }
    public DoorHandler DoorHandler { get; private set; }

    [SerializeField]
    private SceneLoader sceneLoader;

    [SerializeField]
    private float gameTime;

    #region Quest Components
    public QuestManager QuestManager { get; private set; }

    [SerializeField]
    private QuestData[] questData = new QuestData[5];
    #endregion


    private void Awake()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Timer = new GameTimer(gameTime);

        QuestManager = new QuestManager(questData, this);
        DialogueActionHandler = new DialogueActionHandler(PlayerRef, this);
        DoorHandler = new DoorHandler(PlayerRef, this);

        sceneLoader.GetComponent<Animator>().Play("SCENELOADER_FADEOUT");
    }

    private void Update()
    {
        QuestManager.IsQuestComplete();

        if (Timer.HasTimeExpired())
        {
            StaticGameData.CompletedWithinTime = false;
            sceneLoader.LoadGameOver();
        }
    }

    public void GameCompleted()
    {
        StaticGameData.CompletedWithinTime = true;
        sceneLoader.LoadGameOver();
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
        DialogueActionHandler.CheckWhenToUpdate(objToUpdate);
    }

    public void UpdateDialogueFinish(bool isDialogueFinished)
    {
        DialogueActionHandler.UpdateDialogueFinish(isDialogueFinished);
    }
    #endregion
}
