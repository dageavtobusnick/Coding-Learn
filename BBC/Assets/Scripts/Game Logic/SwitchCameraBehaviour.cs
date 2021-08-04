using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraBehaviour : MonoBehaviour
{
    [Header ("Камеры для переключения")]
    [Tooltip ("Камера, которая будет выключена")]
    public Camera PreviousCamera;
    [Tooltip("Камера, которая будет включена")]
    public Camera NextCamera;

    private GameObject Canvas;
    private GameData gameData;

    private void OnTriggerEnter(Collider other)
    {
        PreviousCamera.enabled = false;
        NextCamera.enabled = true;
        gameData.CurrentSceneCamera = NextCamera;
    }

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
        gameData = Canvas.GetComponent<GameData>();
    }
}
