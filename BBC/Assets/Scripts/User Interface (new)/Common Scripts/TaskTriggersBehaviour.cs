using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTriggersBehaviour : MonoBehaviour
{
    private GameObject activateTaskButton;
    private GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        var triggerName = other.gameObject.name;
        if (triggerName.StartsWith("TaskTrigger"))
        {
            activateTaskButton.SetActive(true);
            canvas.GetComponent<GameData>().currentTaskNumber = int.Parse(triggerName[triggerName.Length - 1].ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        activateTaskButton.SetActive(false);
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        activateTaskButton = GameObject.Find("ActivateTaskButton");
        activateTaskButton.SetActive(false);
    } 
}
