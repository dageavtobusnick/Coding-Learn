using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TaskPanelBehaviour : MonoBehaviour
{
    [Header ("Номер текущего задания")]
    public int TaskNumber;
    [Header ("Интерфейс")]
    public GameObject Canvas;
    
    private InterfaceElements UI;
    private GameData gameData;
    private PadBehaviour padBehaviour;
    private PlayerBehaviour robotBehaviour;

    public void ShowTask()
    {
        var taskText = gameData.TaskTexts[TaskNumber - 1];
        UI.ExtendedTaskTitle.text = taskText.Title;
        UI.ExtendedTaskDescription.text = taskText.ExtendedDescription;
        UI.TaskTitle.text = taskText.Title;
        UI.TaskDescription.text = taskText.Description;
        padBehaviour.StartCode = taskText.StartCode;
        UI.CodeField.text = taskText.StartCode;
        UI.ResultField.text = "";
        UI.OutputField.text = "";
        Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
        Canvas.GetComponent<StartButtonBehaviour>().TaskNumber = TaskNumber;
    }

    public void CloseTask() => StartCoroutine(CloseTask_COR());

    public IEnumerator ReturnToScene_COR()
    {
        gameData.IsTaskStarted = false;
        gameData.CurrentSceneCamera.GetComponent<PlayableDirector>().playableAsset = Resources.Load<PlayableAsset>("Timelines/Tasks/Level " + gameData.SceneIndex + "/ReturnToScene_Task_" + TaskNumber);
        gameData.CurrentSceneCamera.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(2f);
        var isTaskCompleted = gameData.HasTasksCompleted[TaskNumber - 1];
        if (!isTaskCompleted)
        {
            var activatedTrigger = Canvas.GetComponent<ActionButtonBehaviour>().ActivatedTrigger.gameObject;
            gameData.Player.GetComponentInChildren<TriggersBehaviour>().ActivateTrigger_Any(activatedTrigger);
            StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().ShowActionButton_COR());
        }
        robotBehaviour.UnfreezePlayer();
        UI.Minimap.SetActive(true);
        UI.ShowIDEButton.interactable = false;
        UI.ChangeCallAvailability(true);
        Canvas.GetComponent<ActionButtonBehaviour>().IsPressed = false;
        padBehaviour.Mode = PadBehaviour.PadMode.Normal;
    }

    private IEnumerator CloseTask_COR()
    {     
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        if (gameData.SceneIndex != 0)
            yield return StartCoroutine(ReturnToScene_COR());  
    }

    private void Awake()
    {
        gameData = Canvas.GetComponent<GameData>();
        UI = Canvas.GetComponent<InterfaceElements>();
        padBehaviour = Canvas.GetComponent<PadBehaviour>();
        robotBehaviour = gameData.Player.GetComponent<PlayerBehaviour>();
        UI.NextLevelButton.gameObject.SetActive(false);
    }
}
