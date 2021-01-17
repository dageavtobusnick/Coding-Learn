using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonLevel3Behaviour : MonoBehaviour
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
Помоги мне с олимпиадой по пингвиному программированию
Даны два числа (одно гарантированно больше другого). Меньшее из этих чисел заменить суммой данных чисел, большее - произведением.
Пример ввода: 12, 32
Пример вывода: 44, 384";
        ShowTask();
    }

    public void ShowTask2()
    {
        taskNumber = 2;
        taskDescription = @"                                                               Задание
Тут такое дело, я прибыл сюда из Южного полюса, меня интересует, как склонять мои монетки на русском.
Напишите метод, склоняющий существительное «рублей» следующее за указанным числительным. Например, для аргумента 10, метод должен вернуть «рублей», для 1 — вернуть «рубль», для 2 — «рубля».";
        ShowTask();
    }

    public void ShowTask3()
    {
        taskNumber = 3;
        taskDescription = @"                                                               Задание
числа - это пингвины, а терперь давай каждого взрослого пингвина замени на 2 малышей.
Даны целые числа a, b, c. Если a ≤ b ≤ c, то все числа заменить их квадратами, в противном случае сменить знак каждого числа.
Пример ввода: 3, 4, 4
Пример вывода: 9, 16, 16";
        ShowTask();
    }

    public void ShowTask4()
    {
        taskNumber = 4;
        taskDescription = @"                                                               Задание
Нужно посчитать число пингвинов на всех территориях здесь. Найти, какое из них является средним (больше одного, но меньше другого).
Пример ввода: 12, 32, 7
Пример вывода: 12";
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
