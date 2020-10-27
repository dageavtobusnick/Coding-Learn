using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton4Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject panel;
    InputField taskField;
    InputField field;
    int taskNumber = 4;
    string taskDescription = "Напишите метод, склоняющий существительное «рублей» следующее за указанным числительным. Например, для аргумента 10, метод должен вернуть «рублей», для 1 — вернуть «рубль», для 2 — «рубля».";
    string taskSignature = @"
using System;

namespace YourSolution
{
    class YourClass
    {
        public string YourMethod(int roublesCount)
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
