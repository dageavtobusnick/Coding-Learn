using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton3Behaviour : MonoBehaviour
{
    GameObject player;
    GameObject panel;
    InputField taskField;
    InputField field;
    int taskNumber = 3;
    string taskDescription = "Даны целые числа a, b, c. Если a ≤ b ≤ c, то все числа заменить их квадратами, если a>b>c, то каждое число заменить наибольшим из них, в противном случае сменить знак каждого числа.";
    string taskSignature = @"
using System;

namespace YourSolution
{
    class YourClass
    {
        public Tuple<int, int, int> YourMethod(int a, int b, int c)
        {
            //как и в первом задании, return готов, осталось реализовать алгоритм
            
            return Tuple.Create(a, b, c);
        }
    }
}";

    public void ShowTask()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        panel.transform.position = panel.GetComponent<PanelBehaviour>().TurnOnPosition;
        taskField.text = taskDescription;
        field.text = taskSignature;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber = taskNumber;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskSignature = taskSignature;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        panel = GameObject.Find("Panel");
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        field = GameObject.Find("InputField").GetComponent<InputField>();
    }
}
