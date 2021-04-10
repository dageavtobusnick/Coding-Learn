using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Camera currentSceneCamera;
    public int currentTaskNumber;

    private void Start()
    {
        currentSceneCamera = GameObject.Find("SceneCamera_1").GetComponent<Camera>();
    }
}
