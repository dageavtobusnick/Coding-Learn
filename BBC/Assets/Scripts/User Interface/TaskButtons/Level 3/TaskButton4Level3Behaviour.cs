using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton4Level3Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject codeField;
    InputField taskField;
    int taskNumber = 4;
    string taskDescription = @"                                                               Задание
Вводятся три разных числа. Найти, какое из них является средним (больше одного, но меньше другого).
Пример ввода: 12, 32, 7
Пример вывода: 12";

    public void ShowTask()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        codeField.transform.position = codeField.GetComponent<PanelTask4Behaviour>().TurnOnPosition;
        taskField.text = taskDescription;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        codeField = GameObject.Find("Panel_Task" + taskNumber);
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
    }
}
