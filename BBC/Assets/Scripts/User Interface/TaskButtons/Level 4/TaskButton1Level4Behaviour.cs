using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton1Level4Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject codeField;
    InputField taskField;
    int taskNumber = 1;
    string taskDescription = @"                                                               Задание
Вводится натуральное число. Найти сумму четных цифр, входящих в его состав.
Пример ввода: 12324
Пример вывода: 8
";

    public void ShowTask()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        codeField.transform.position = codeField.GetComponent<PanelTask1Behaviour>().TurnOnPosition;
        taskField.text = taskDescription;
    }

    private void Start()
    {
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        codeField = GameObject.Find("Panel_Task" + taskNumber);
        player = GameObject.Find("Snowman");
    }
}
