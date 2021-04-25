using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggersBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

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
    }

    private void OnTriggerExit(Collider other) => StartCoroutine(DeleteButton_COR());

    private IEnumerator DeleteButton_COR()
    {
        yield return StartCoroutine(UIAnimations.HideActivateTaskButton_COR());
        UI.ActivateTaskButton.gameObject.SetActive(false);
    }

    void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        UI.ActivateTaskButton.gameObject.SetActive(false);
    } 
}
