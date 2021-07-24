using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private int sceneIndex;
    private GameData gameData;

    public void ActivateDialogCamera_Animated(int cameraNumber)
    {
        ActivateDialogCamera_Static(cameraNumber);
        var characterName = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger.alias;
        gameData.CurrentDialogCamera.GetComponent<Animator>().Play("MoveDialogCamera_" + characterName + "_" + cameraNumber);
    }

    public void ActivateDialogCamera_Static(int cameraNumber)
    {
        var dialogCameras = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger.transform.parent.GetChild(2);
        var newDialogCamera = dialogCameras.GetChild(cameraNumber - 1).gameObject.GetComponent<Camera>();
        newDialogCamera.enabled = true;
        if (gameData.CurrentDialogCamera != null)
            gameData.CurrentDialogCamera.enabled = false;
        gameData.CurrentDialogCamera = newDialogCamera;
    }

    public void ActivateTask_PostDialog(int taskNumber)
    {
        gameData.CurrentTaskNumber = taskNumber;
        Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask(false);
    }

    public void ActivateTrigger_NPC(int npcOrderNumber)
    {
        var npcTrigger = gameData.Player.GetComponent<TriggersBehaviour>().DialogCharacters.transform.GetChild(npcOrderNumber - 1).GetChild(1).gameObject;
        npcTrigger.SetActive(true);
        npcTrigger.GetComponentInChildren<Animator>().Play("RotateExclamationMark");
    }

    public void ActivateTrigger_Task(int triggerNumber)
    {
        var taskTrigger = Canvas.GetComponent<TriggersBehaviour>().TaskTriggers.transform.GetChild(triggerNumber - 1).gameObject;
        taskTrigger.SetActive(true);
        taskTrigger.GetComponentInChildren<Animator>().Play("RotateExclamationMark");
    }

    public void DeactivateDialogCamera() => gameData.CurrentDialogCamera.enabled = false;

    public void LeaveNPC() => gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger.gameObject.SetActive(false);

    public void ShowNextPlace_NPC(int npcOrderNumber) => gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_NPC_" + npcOrderNumber);

    public void ShowNextPlace_TaskTrigger(int triggerNumber) => gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_TaskTrigger_" + triggerNumber);

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
