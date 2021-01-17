using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonLevel1Behaviour : MonoBehaviour
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
У тебя наверное сразу возник вопрос, что ты здесь забыл?
Не буду дожидаться ответа просто скажу такую вещь, 
выполнишь для меня пару заданий и я тебе дам один из 4-ех ключей от той вековой двери
Зачем тебе идти дальше?
Если ты не справишься и не найдешь все ключи, я позову медведя и он тебя съест
";
        ShowTask();
    }

    public void ShowTask2()
    {
        taskNumber = 2;
        taskDescription = @"                                                               Задание
Приятель, тут такое дело, ни как не могу запустить механизм, чтобы добыть рыбу, можешь помочь?";
        ShowTask();
    }

    public void ShowTask3()
    {
        taskNumber = 3;
        taskDescription = @"                                                               Задание
Надеюсь, ты знаешь курс валют, реши задания";
        ShowTask();
    }

    public void ShowTask4()
    {
        taskNumber = 4;
        taskDescription = @"                                                               Задание
Что ты думаешь на счет того, чтобы помочь?
У меня 4-ый ключ, от тебя зависит попадешь ли ты на следующий уровень или тебя съест медведь";
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
