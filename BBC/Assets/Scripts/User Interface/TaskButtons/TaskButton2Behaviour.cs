using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton2Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject panel;
    InputField taskField;
    InputField field;
    int taskNumber = 2;
    string taskDescription = "Вводятся три разных числа. Найти, какое из них является средним (больше одного, но меньше другого).";
    string taskSignature = @"
using System;

namespace YourSolution
{
    class YourClass
    {
        public int YourMethod(int a, int b, int c)
        {
        
        }
    }
}";

    public void ShowTask()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        panel.transform.position = panel.GetComponent<PanelBehaviour>().TurnOnPosition;
        taskField.text = taskDescription;
        field.text = taskSignature;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber = taskNumber;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskSignature = taskSignature;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        panel = GameObject.Find("Panel");
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        field = GameObject.Find("InputField").GetComponent<InputField>();
    }
}
