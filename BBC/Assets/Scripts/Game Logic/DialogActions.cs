using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActions : MonoBehaviour
{
    [Header("Èםעונפויס")]
    public GameObject Canvas;
    [HideInInspector]
    public VIDE_Assign CurrentNPC;

    private GameData gameData;
    private TriggersBehaviour triggersBehaviour;

    public void ActivateDialogCamera_Animated(int cameraNumber)
    {
        ActivateDialogCamera_Static(cameraNumber);
        var characterName = CurrentNPC.alias;
        gameData.CurrentDialogCamera.GetComponent<Animator>().Play("MoveDialogCamera_" + characterName + "_" + cameraNumber);
    }

    public void ActivateDialogCamera_Static(int cameraNumber)
    {
        var dialogCameras = CurrentNPC.transform.parent.GetChild(2);
        var newDialogCamera = dialogCameras.GetChild(cameraNumber - 1).gameObject.GetComponent<Camera>();
        newDialogCamera.enabled = true;
        if (gameData.CurrentDialogCamera != null)
            gameData.CurrentDialogCamera.enabled = false;
        gameData.CurrentDialogCamera = newDialogCamera;
    }

    public void ActivateNpcAnimation(string animationName) => CurrentNPC.gameObject.transform.parent.GetComponentInChildren<Animator>().Play(animationName);

    public void ActivateTask_PostDialog(int taskNumber)
    {
        gameData.CurrentTaskNumber = taskNumber;
        StartCoroutine(Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask_COR(false));
    }

    public void ActivateTrigger_ChangeScene(int triggerNumber) => triggersBehaviour.ActivateTrigger_ChangeScene(triggerNumber);

    public void ActivateTrigger_NPC(int npcOrderNumber) => triggersBehaviour.ActivateTrigger_Dialogue(npcOrderNumber);

    public void ActivateTrigger_Task(int triggerNumber) => triggersBehaviour.ActivateTrigger_Task(triggerNumber);

    public void ChangeTarget(string target) => Canvas.GetComponent<TargetPanelBehaviour>().ChangeTarget(target);

    public void DeactivateDialogCamera() => gameData.CurrentDialogCamera.enabled = false;

    public void LeaveNPC()
    {
        CurrentNPC.gameObject.SetActive(false);
        triggersBehaviour.DeleteActionButton();
        Canvas.GetComponent<ActionButtonBehaviour>().IsPressed = false;
    }

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        triggersBehaviour = gameData.Player.GetComponentInChildren<TriggersBehaviour>();
    }
}
