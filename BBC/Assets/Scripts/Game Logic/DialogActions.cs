using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private int sceneIndex;
    private GameData gameData;
    private GameObject player;
    private TriggersBehaviour triggersBehaviour;

    public void ActivateDialogCamera_Animated(int cameraNumber)
    {
        ActivateDialogCamera_Static(cameraNumber);
        var characterName = player.GetComponent<VIDEDemoPlayer>().inTrigger.alias;
        gameData.CurrentDialogCamera.GetComponent<Animator>().Play("MoveDialogCamera_" + characterName + "_" + cameraNumber);
    }

    public void ActivateDialogCamera_Static(int cameraNumber)
    {
        var dialogCameras = player.GetComponent<VIDEDemoPlayer>().inTrigger.transform.parent.GetChild(2);
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

    public void ActivateTrigger_ChangeScene(int triggerNumber) => ActivateTrigger(triggersBehaviour.ChangeSceneTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ActivateTrigger_NPC(int npcOrderNumber) => ActivateTrigger(triggersBehaviour.DialogCharacters.transform.GetChild(npcOrderNumber - 1).GetChild(1).gameObject);

    public void ActivateTrigger_Task(int triggerNumber) => ActivateTrigger(triggersBehaviour.TaskTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ChangeTarget(string target) => Canvas.GetComponent<TargetPanelBehaviour>().ChangeTarget(target);

    public void DeactivateDialogCamera() => gameData.CurrentDialogCamera.enabled = false;

    public void LeaveNPC() => player.GetComponent<VIDEDemoPlayer>().inTrigger.gameObject.SetActive(false);

    public void ShowNextPlace_NPC(int npcOrderNumber) => gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_NPC_" + npcOrderNumber);

    public void ShowNextPlace_TaskTrigger(int triggerNumber) => gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_TaskTrigger_" + triggerNumber);

    public void ChangeDialogStartNode()
    {
        var npc = player.GetComponent<VIDEDemoPlayer>().inTrigger;
        switch (npc.alias)
        {
            case "Дровосек":
                if (npc.overrideStartNode == 5)
                    npc.overrideStartNode = 0;
                break;
        }
    } 

    private void ActivateTrigger(GameObject trigger)
    {
        trigger.SetActive(true);
        trigger.GetComponentInChildren<Animator>().Play("RotateExclamationMark");
    }

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        player = gameData.Player;
        triggersBehaviour = player.GetComponent<TriggersBehaviour>();
        sceneIndex = gameData.SceneIndex;
    }

}
