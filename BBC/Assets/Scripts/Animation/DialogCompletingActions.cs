using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCompletingActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private int sceneIndex;
    private GameData gameData;

    public void ActivateTask_PostDialog(int taskNumber)
    {
        gameData.CurrentTaskNumber = taskNumber;
        Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask(false);
    }

    public void ActivateTrigger_Task(int triggerNumber)
    {
        var taskTrigger = Canvas.GetComponent<TriggersBehaviour>().TaskTriggers.transform.GetChild(triggerNumber - 1).gameObject;
        taskTrigger.SetActive(true);
        taskTrigger.GetComponentInChildren<Animator>().Play("RotateExclamationMark");
        gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_TaskTrigger_" + triggerNumber);
    }

    public void ActivateTrigger_NPC(int npcOrderNumber)
    {
        var npcTrigger = gameData.Player.GetComponent<TriggersBehaviour>().DialogCharacters.transform.GetChild(npcOrderNumber - 1).GetChild(1).gameObject;
        npcTrigger.SetActive(true);
        npcTrigger.GetComponentInChildren<Animator>().Play("RotateExclamationMark");
        gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_NPC_" + npcOrderNumber);
    }

    public void LeaveNPC() => gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger.gameObject.SetActive(false);

    public void ChangeDialogStartNode()
    {
        var npc = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger;
        switch (npc.alias)
        {
            case "Дровосек":
                if (npc.overrideStartNode == 5)
                    npc.overrideStartNode = 0;
                break;
        }
    } 

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        sceneIndex = gameData.SceneIndex;
    }

}
