using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonBehaviour : MonoBehaviour
{
    GameObject player;
    GameObject codeField;
    InputField taskField;
    int taskNumber = 1;
    string taskDescription = @"
Даны два числа. Меньшее из этих чисел заменить суммой данных чисел, большее - произведением.
Пример ввода: 12, 32
Пример вывода: 44, 384";

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
