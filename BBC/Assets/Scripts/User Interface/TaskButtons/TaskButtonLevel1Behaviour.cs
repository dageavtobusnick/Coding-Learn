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
Ответьте на предложенные вопросы.";
        ShowTask();
    }

    public void ShowTask2()
    {
        taskNumber = 2;
        taskDescription = @"                                                               Задание
Ответьте на предложенные вопросы.";
        ShowTask();
    }

    public void ShowTask3()
    {
        taskNumber = 3;
        taskDescription = @"                                                               Задание
Ответьте на предложенные вопросы.";
        ShowTask();
    }

    public void ShowTask4()
    {
        taskNumber = 4;
        taskDescription = @"                                                               Задание
Ответьте на предложенные вопросы.";
        ShowTask();
    }

    private void ShowTask()
    {
        codeField = GameObject.Find("Panel_Task" + taskNumber);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        codeField.transform.position = codeField.GetComponent<PanelTaskBehaviour>().TurnOnPosition;
        taskField.text = taskDescription;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
    }
}
