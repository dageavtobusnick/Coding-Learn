using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonLevel5Behaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject codeField;
    private InputField taskField;
    private int taskNumber;
    private string taskDescription;

    public void ShowTask1()
    {
        taskNumber = 1;
        taskDescription = @"                                                               Задание
Ответьте на вопрос и решите задачу.";
        ShowTask();
    }

    public void ShowTask2()
    {
        taskNumber = 2;
        taskDescription = @"                                                               Задание
Создайте массив, содержащий числа 1, 2, 3, 4, 5, 6.";
        ShowTask();
    }

    public void ShowTask3()
    {
        taskNumber = 3;
        taskDescription = @"                                                               Задание
Дан массив array = int[5]. Присвойте первому элементу значение 10, а элементам с индексами 2 и 3 - значение 20.";
        ShowTask();
    }

    public void ShowTask4()
    {
        taskNumber = 4;
        taskDescription = @"                                                               Задание
Дан массив array = {1, 3, 5, 7, 9}. Увеличьте значение каждого элемента на 3, а затем выведите их кубы (число в 3-ей степени).";
        ShowTask();
    }

    private void ShowTask()
    {
        codeField = GameObject.Find("Panel_Task" + taskNumber);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        codeField.transform.position = codeField.GetComponent<PanelTaskBehaviour>().TurnOnPosition;
        taskField.text = taskDescription;
        foreach (var answers in codeField.GetComponentsInChildren<InputField>())
            answers.interactable = true;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        var taskPanels = GameObject.Find("Task Panels");
        foreach (var answers in taskPanels.GetComponentsInChildren<InputField>())
            answers.interactable = false;
    }
}
