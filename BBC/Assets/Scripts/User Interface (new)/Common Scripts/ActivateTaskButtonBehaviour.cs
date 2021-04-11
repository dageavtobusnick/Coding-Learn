using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTaskButtonBehaviour : MonoBehaviour
{
    private GameObject canvas;
    private RobotBehaviour robotBehaviour;

    public void ActivateTask()
    {
        var currentTaskNumber = canvas.GetComponent<GameData>().currentTaskNumber;
        canvas.GetComponent<TaskPanelBehaviour>().taskNumber = currentTaskNumber;
        canvas.GetComponent<TaskPanelBehaviour>().ChangeTask();
        var camera = GameObject.Find("TaskCamera_" + currentTaskNumber);
        if (camera != null)
            camera.GetComponent<Camera>().enabled = true;
        gameObject.SetActive(false);
        robotBehaviour.currentMoveSpeed = robotBehaviour.freezeSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.freezeSpeed;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        robotBehaviour = GameObject.Find("robot1").GetComponent<RobotBehaviour>();
    }
}
