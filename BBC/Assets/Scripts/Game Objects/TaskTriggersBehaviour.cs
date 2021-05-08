using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTriggersBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;
    [Header("Триггеры заданий")]
    public GameObject TaskTriggers;
    [Header("Триггеры смены сцен")]
    public GameObject EnterTriggers;
    [Header("Триггеры активации сценарного момента")]
    public GameObject ScenarioTriggers;

    private InterfaceElements UI;
    private InterfaceAnimations UIAnimations;

    private void OnTriggerEnter(Collider other)
    {
        var triggerName = other.gameObject.name;
        if (triggerName.StartsWith("TaskTrigger"))
        {
            var taskNumber = int.Parse(triggerName[triggerName.Length - 1].ToString());
            var isTaskCompleted = Canvas.GetComponent<TaskCompletingActions>().isTasksCompleted[taskNumber - 1];
            if (!isTaskCompleted)
            {
                UI.ActivateTaskButton.gameObject.SetActive(true);
                StartCoroutine(UIAnimations.ShowActivateTaskButton_COR());
                Canvas.GetComponent<GameData>().currentTaskNumber = taskNumber;
            }
        }
        else if (triggerName.StartsWith("EnterTrigger"))
        {
            UI.ChangeSceneButton.gameObject.SetActive(true);
            var buttonText = other.gameObject.GetComponent<SwitchSceneBehaviour>().buttonText;
            UI.ChangeSceneButton.GetComponentInChildren<Text>().text = buttonText;
            StartCoroutine(UIAnimations.ShowChangeSceneButton_COR());
            Canvas.GetComponent<GameData>().currentChangeSceneTriggerNumber = int.Parse(triggerName[triggerName.Length - 1].ToString());
        }
        else if (triggerName.StartsWith("ScenarioTrigger"))
        {
            UI.ScenarioButton.gameObject.SetActive(true);
            StartCoroutine(UIAnimations.ShowScenarioButton_COR());
            Canvas.GetComponent<GameData>().currentScenarioTriggerNumber = int.Parse(triggerName[triggerName.Length - 1].ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var triggerName = other.gameObject.name;
        if (triggerName.StartsWith("TaskTrigger"))
            StartCoroutine(DeleteActivateTaskButton_COR());
        else if (triggerName.StartsWith("EnterTrigger"))
            StartCoroutine(DeleteChangeSceneButton_COR());
        else if (triggerName.StartsWith("ScenarioTrigger"))
            StartCoroutine(DeleteScenarioButton_COR());
    }

    private IEnumerator DeleteActivateTaskButton_COR()
    {
        yield return StartCoroutine(UIAnimations.HideActivateTaskButton_COR());
        UI.ActivateTaskButton.gameObject.SetActive(false);
    }

    private IEnumerator DeleteChangeSceneButton_COR()
    {
        yield return StartCoroutine(UIAnimations.HideChangeSceneButton_COR());
        UI.ChangeSceneButton.gameObject.SetActive(false);
    }

    private IEnumerator DeleteScenarioButton_COR()
    {
        yield return StartCoroutine(UIAnimations.HideScenarioButton_COR());
        UI.ScenarioButton.gameObject.SetActive(false);
    }

    private void RotateMarks(GameObject triggers)
    {
        for (var i = 0; i < triggers.transform.childCount; i++)
        {
            var currentChild = triggers.transform.GetChild(i);
            currentChild.GetChild(0).GetChild(0).GetComponent<Animator>().Play("RotateExclamationMark");
        }
    }
    
    void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        UI.ActivateTaskButton.gameObject.SetActive(false);
        UI.ChangeSceneButton.gameObject.SetActive(false);
        RotateMarks(TaskTriggers);
        RotateMarks(EnterTriggers);
        RotateMarks(ScenarioTriggers);
    } 
}
