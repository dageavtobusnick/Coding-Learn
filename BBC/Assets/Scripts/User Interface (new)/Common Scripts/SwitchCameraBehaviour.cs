using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraBehaviour : MonoBehaviour
{
    private GameObject canvas;
    private List<Camera> cameras = new List<Camera>();
    private List<Camera> taskCameras = new List<Camera>();
    private bool isCameraChanged;
    private int step = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCameraChanged)
        {
            TurnOnAnotherCamera(step);
            step = step == 1 ? -1 : 1;
            isCameraChanged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCameraChanged = false;
    }

    private void TurnOnAnotherCamera(int changeStep)
    {
        for (var i = 0; i < cameras.Count; i++)
        {
            if (cameras[i].enabled)
            {
                cameras[i + changeStep].enabled = true;
                cameras[i].enabled = false;
                isCameraChanged = true;
                canvas.GetComponent<GameData>().currentSceneCamera = cameras[i + changeStep];
                break;
            }
        }
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        var sceneCamerasCount = GameObject.Find("SceneCameras").transform.childCount;
        var taskCamerasCount = GameObject.Find("TaskCameras").transform.childCount;
        for (var i = 1; i <= sceneCamerasCount; i++)
            cameras.Add(GameObject.Find("SceneCamera_" + i).GetComponent<Camera>());
        for (var i = 1; i <= taskCamerasCount; i++)
            taskCameras.Add(GameObject.Find("TaskCamera_" + i).GetComponent<Camera>());
    }
}
