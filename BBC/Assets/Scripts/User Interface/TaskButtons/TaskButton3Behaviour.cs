using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton3Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject codeField;
    InputField taskField;
    int taskNumber = 3;
    string taskDescription = @"
Даны целые числа a, b, c. Если a ≤ b ≤ c, то все числа заменить их квадратами, в противном случае сменить знак каждого числа.
Пример ввода: 3, 4, 4
Пример вывода: 9, 16, 16";

    public void ShowTask()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        codeField.transform.position = codeField.GetComponent<PanelTask3Behaviour>().TurnOnPosition;
        taskField.text = taskDescription;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        codeField = GameObject.Find("Panel_Task" + taskNumber);
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
    }
}
