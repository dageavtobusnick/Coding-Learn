using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton2Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject codeField;
    InputField taskField;
    int taskNumber = 2;
    string taskDescription = "Напишите метод, склоняющий существительное «рублей» следующее за указанным числительным. Например, для аргумента 10, метод должен вернуть «рублей», для 1 — вернуть «рубль», для 2 — «рубля».";

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
