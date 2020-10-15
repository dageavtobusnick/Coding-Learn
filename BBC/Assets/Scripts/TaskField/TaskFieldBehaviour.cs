using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskFieldBehaviour : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    /*void Update()
    {
        GameObject taskButton = GameObject.Find("TaskButton");
        if (taskButton.GetComponent<TaskButtonBehaviour>().isClicked == true)
            gameObject.SetActive(true);
    }*/
}
