using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public VIDE_Assign currentNPC;

    private GameData gameData;
    private TriggersBehaviour triggersBehaviour;

    public void ActivateDialogCamera_Animated(int cameraNumber)
    {
        ActivateDialogCamera_Static(cameraNumber);
        var characterName = currentNPC.alias;
        gameData.CurrentDialogCamera.GetComponent<Animator>().Play("MoveDialogCamera_" + characterName + "_" + cameraNumber);
    }

    public void ActivateDialogCamera_Static(int cameraNumber)
    {
        var dialogCameras = currentNPC.transform.parent.GetChild(2);
        var newDialogCamera = dialogCameras.GetChild(cameraNumber - 1).gameObject.GetComponent<Camera>();
        newDialogCamera.enabled = true;
        if (gameData.CurrentDialogCamera != null)
            gameData.CurrentDialogCamera.enabled = false;
        gameData.CurrentDialogCamera = newDialogCamera;
    }

    public void ActivateNpcAnimation(string animationName) => currentNPC.gameObject.transform.parent.GetComponentInChildren<Animator>().Play(animationName);

    public void ActivateTask_PostDialog(int taskNumber)
    {
        gameData.CurrentTaskNumber = taskNumber;
        StartCoroutine(Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask_COR(false));
    }

    public void ActivateTrigger_ChangeScene(int triggerNumber) => ActivateTrigger(triggersBehaviour.ChangeSceneTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ActivateTrigger_NPC(int npcOrderNumber) => ActivateTrigger(triggersBehaviour.DialogCharacters.transform.GetChild(npcOrderNumber - 1).GetChild(1).gameObject);

    public void ActivateTrigger_Task(int triggerNumber) => ActivateTrigger(triggersBehaviour.TaskTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ChangeTarget(string target) => Canvas.GetComponent<TargetPanelBehaviour>().ChangeTarget(target);

    public void DeactivateDialogCamera() => gameData.CurrentDialogCamera.enabled = false;

    public void LeaveNPC()
    {
        currentNPC.gameObject.SetActive(false);
        triggersBehaviour.DeleteActionButton();
    }

    public void ShowNextPlace_NPC(int npcOrderNumber) => gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_NPC_" + npcOrderNumber);

    public void ShowNextPlace_TaskTrigger(int triggerNumber) => gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ShowNextPlace_TaskTrigger_" + triggerNumber);

    public void ChangeDialogStartNode()
    {
        switch (currentNPC.alias)
        {
            case "Дровосек":
                if (currentNPC.overrideStartNode == 5)
                    currentNPC.overrideStartNode = 0;
                break;
        }
    } 

    private void ActivateTrigger(GameObject trigger)
    {
        trigger.SetActive(true);
        trigger.transform.GetChild(0).gameObject.SetActive(true);
        trigger.GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
    }

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        triggersBehaviour = gameData.Player.GetComponentInChildren<TriggersBehaviour>();
    }
}
