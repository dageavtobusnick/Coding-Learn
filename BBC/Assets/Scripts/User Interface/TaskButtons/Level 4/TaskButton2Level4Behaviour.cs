using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton2Level4Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject codeField;
    InputField taskField;
    int taskNumber = 2;
    string taskDescription = @"                                                               Задание
Найти сумму и произведение цифр, введенного натурального числа.
Например, если введено число 325, то сумма его цифр равна 10 (3+2+5), а произведение 30 (3*2*5).";

    public void ShowTask()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        codeField.transform.position = codeField.GetComponent<PanelTask2Behaviour>().TurnOnPosition;
        taskField.text = taskDescription;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        codeField = GameObject.Find("Panel_Task" + taskNumber);
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
    }
}
