using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanelBehaviour : MonoBehaviour
{
    [Header ("Номер текущего задания")]
    public int taskNumber;
    [HideInInspector]
    public int tasksCount;
    [Header ("Интерфейс")]
    public GameObject Canvas;
    
    private int sceneIndex;
    private InterfaceElements UI;
    private GameData gameData;
    private PadBehaviour padBehaviour;
    private RobotBehaviour robotBehaviour;

    public void ShowTask()
    {
        var taskText = gameData.TaskTexts[taskNumber - 1];
        UI.ExtendedTaskTitle.text = taskText.Title;
        UI.ExtendedTaskDescription.text = taskText.ExtendedDescription;
        UI.TaskTitle.text = taskText.Title;
        UI.TaskDescription.text = taskText.Description;
        padBehaviour.StartCode = taskText.StartCode;
        UI.CodeField.text = taskText.StartCode;
        UI.ResultField.text = "";
        UI.OutputField.text = "";
        Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
        UI.StartButton.GetComponent<StartButtonBehaviour>().TaskNumber = taskNumber;
    }

    public void CloseTask() => StartCoroutine(CloseTask_COR());

    public void ReturnToScene() => StartCoroutine(ReturnToScene_COR());

    private IEnumerator CloseTask_COR()
    {
        gameData.IsTaskStarted = false;
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        if (sceneIndex != 0)
        {
            yield return StartCoroutine(ReturnToScene_COR());
            UI.Minimap.SetActive(true);
            UI.IDEButton.interactable = false;
        }
        padBehaviour.Mode = PadBehaviour.PadMode.Normal;
        UI.ChangeCallAvailability(true);
    }

    private IEnumerator ReturnToScene_COR()
    {
        gameData.CurrentSceneCamera.GetComponent<Animator>().Play("ReturnToScene_Task_" + taskNumber);
        yield return new WaitForSeconds(2f);
        padBehaviour.Mode = PadBehaviour.PadMode.Normal;
        var isTaskCompleted = gameData.HasTasksCompleted[taskNumber - 1];
        if (!isTaskCompleted)
        {    
            var activatedTrigger = Canvas.GetComponent<ActionButtonBehaviour>().ActivatedTrigger;
            activatedTrigger.gameObject.SetActive(true);
            activatedTrigger.GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
            if (activatedTrigger.TriggerPurpose == TriggerData.Purpose.Dialog)
                Canvas.GetComponentInChildren<DialogActions>().ChangeDialogStartNode();
            StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().ShowActionButton_COR());
        }
        robotBehaviour.UnfreezePlayer();
    }

    private void Awake()
    {
        gameData = Canvas.GetComponent<GameData>();
        UI = Canvas.GetComponent<InterfaceElements>();
        sceneIndex = gameData.SceneIndex;
        padBehaviour = UI.Pad.GetComponent<PadBehaviour>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
        UI.NextLevelButton.gameObject.SetActive(false);
        tasksCount = gameData.TaskTexts.Length;
    }
}
