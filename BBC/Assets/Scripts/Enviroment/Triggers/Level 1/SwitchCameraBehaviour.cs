using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraBehaviour : MonoBehaviour
{
    private Camera camera1;
    private Camera camera2;
    private Camera camera3;

    private void OnTriggerEnter(Collider other)
    {
        if (camera1.enabled)
        {
            camera1.enabled = false;
            camera2.enabled = true;
        }
        else if (camera2.enabled)
        {
            camera2.enabled = false;
            camera3.enabled = true;
        }
    }

    private void Start()
    {
        camera1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        camera2 = GameObject.Find("Camera_2").GetComponent<Camera>();
        camera3 = GameObject.Find("Camera_3").GetComponent<Camera>();

    }
}
