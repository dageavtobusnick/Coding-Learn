using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonLevel4Behaviour : MonoBehaviour
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
Вводится натуральное число. Найти сумму четных цифр, входящих в его состав.
Пример ввода: 12324
Пример вывода: 8";
        ShowTask();
    }

    public void ShowTask2()
    {
        taskNumber = 2;
        taskDescription = @"                                                               Задание
Найти сумму и произведение цифр, введенного натурального числа.
Например, если введено число 325, то сумма его цифр равна 10 (3+2+5), а произведение 30 (3*2*5).";
        ShowTask();
    }

    public void ShowTask3()
    {
        taskNumber = 3;
        taskDescription = @"                                                               Задание
Используя циклы, вывести сначала нечётные числа до 20, а затем их квадраты.";
        ShowTask();
    }

    public void ShowTask4()
    {
        taskNumber = 4;
        taskDescription = @"                                                               Задание
Напишите программу, которая выводит на консоль таблицу умножения";
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
