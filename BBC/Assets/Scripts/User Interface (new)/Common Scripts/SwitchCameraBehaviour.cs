using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraBehaviour : MonoBehaviour
{
    public Camera PreviousCamera;
    public Camera NextCamera;
    private GameObject canvas;
    private bool isCameraChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCameraChanged)
        {
            if (PreviousCamera.enabled)
            {
                PreviousCamera.enabled = false;
                NextCamera.enabled = true;
                canvas.GetComponent<GameData>().currentSceneCamera = NextCamera;
            }
            else
            {
                PreviousCamera.enabled = true;
                NextCamera.enabled = false;
                canvas.GetComponent<GameData>().currentSceneCamera = PreviousCamera;
            }
            isCameraChanged = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCameraChanged = false;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
}
