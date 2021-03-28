using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTaskButton_Level_1_Behaviour : MonoBehaviour
{
    private GameObject canvas;

    public void ActivateTask()
    {
        var currentTaskNumber = canvas.GetComponent<GameData>().currentTaskNumber;
        canvas.GetComponent<TaskPanel_Level_1_Behaviour>().taskNumber = currentTaskNumber;
        canvas.GetComponent<TaskPanel_Level_1_Behaviour>().ChangeTask();
        var camera = GameObject.Find("TaskCamera_" + currentTaskNumber);
        if (camera != null)
            camera.GetComponent<Camera>().enabled = true;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
}
