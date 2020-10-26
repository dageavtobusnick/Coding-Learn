using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonBehaviour : MonoBehaviour
{
    GameObject player;
    GameObject panel;
    InputField taskField;
    InputField field;
    int taskNumber = 1;
    string taskDescription = "Даны два числа. Меньшее из этих чисел заменить суммой данных чисел, большее - произведением.";
    string taskSignature = @"
using System;

namespace YourSolution
{
    class YourClass
    {
        public Tuple<int, int> YourMethod(int a, int b)
        {
            //не беспокойся о строчке return. Просто подумай,
            //как преобразовать a и b.
            
            return Tuple.Create(a, b);
        }
    }
}";

    public void ShowTask()
    {
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        player = GameObject.Find("Snowman");
        panel = GameObject.Find("Panel");
        field = GameObject.Find("InputField").GetComponent<InputField>();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        panel.transform.position = panel.GetComponent<PanelBehaviour>().TurnOnPosition;
        taskField.text = taskDescription;
        field.text = taskSignature;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber = taskNumber;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskSignature = taskSignature;
    }
}
