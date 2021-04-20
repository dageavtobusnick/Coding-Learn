using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (taskNumber <= tasksCount)
        {
            var taskText = gameData.TaskTexts[taskNumber - 1];
            UI.TaskTitle.text = taskText.Title;
            UI.ExtendedTaskTitle.text = taskText.Title;
            UI.TaskDescription.text = taskText.Description;
            UI.ExtendedTaskDescription.text = taskText.ExtendedDescription;
            padBehaviour.StartCode = taskText.StartCode;
            UI.CodeField.text = taskText.StartCode;
            UI.ResultField.text = "";
            UI.OutputField.text = "";
            Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
            UI.StartButton.GetComponent<StartButtonBehaviour>().taskNumber = taskNumber;
        }
        else
        {
            var finishMessage = gameData.FinishMessages[sceneIndex];
            UI.CloseExtendedTaskButton.gameObject.SetActive(false);
            UI.NextLevelButton.gameObject.SetActive(true);
            Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
            UI.ExtendedTaskTitle.text = finishMessage.Title;
            UI.ExtendedTaskDescription.text = finishMessage.Description;
        }
    }

    public void CloseTask() => StartCoroutine(CloseTask_COR());

    private IEnumerator CloseTask_COR()
    {
        UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
        padBehaviour.GetComponent<Animator>().Play("MoveRight_Pad");
        if (sceneIndex != 0)
        {
            UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.7f);
            Canvas.GetComponent<GameData>().currentSceneCamera.GetComponent<Animator>().Play("MoveToScene_TaskCamera_" + taskNumber);
            yield return new WaitForSeconds(2f);
            var isTaskCompleted = Canvas.GetComponent<TaskCompletingActions>().isTasksCompleted[taskNumber - 1];
            if (!isTaskCompleted)
            {
                UI.ActivateTaskButton.GetComponent<Animator>().Play("ScaleInterfaceUp");
                yield return new WaitForSeconds(0.75f);
            }
            robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
            robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
        }
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        sceneIndex = gameData.SceneIndex;
        padBehaviour = UI.Pad.GetComponent<PadBehaviour>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
        UI.NextLevelButton.gameObject.SetActive(false);
        tasksCount = gameData.TaskTexts.Length;
    }
}
