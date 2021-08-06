using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void ChangeTask()
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
        UI.StartButton.GetComponent<StartButtonBehaviour>().taskNumber = taskNumber;
    }

    public void CloseTask() => StartCoroutine(CloseTask_COR());

    public void ReturnToScene() => StartCoroutine(ReturnToScene_COR());

    private IEnumerator CloseTask_COR()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
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
            if (Canvas.GetComponent<ActionButtonBehaviour>().ActivatedTrigger.TriggerPurpose == TriggerData.Purpose.Dialog)
            {
                var npcMark = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger.transform.GetChild(0).gameObject;
                npcMark.SetActive(true);
                npcMark.GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
                Canvas.GetComponentInChildren<DialogActions>().ChangeDialogStartNode();
            }
            else
            {
                var taskMark = Canvas.GetComponent<ActionButtonBehaviour>().ActivatedTrigger;
                taskMark.gameObject.SetActive(true);
                taskMark.GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
            }          
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
