using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton2Behaviour : MonoBehaviour
{
    GameObject player;
    InputField taskField;
    InputField field;
    int taskNumber = 2;
    string taskDescription = "Следующая задача сложнее - найди произведение заданных чисел a и b (a > b), частное от деления большего на меньшее, а затем верни разность полученных результатов.";
    string taskSignature = @"
using System;

namespace YourSolution
{
    class YourClass
    {
        public int YourMethod(int a, int b)
        {
        
        }
    }
}";

    public void ShowTask()
    {
        player = GameObject.Find("Snowman");
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        field = GameObject.Find("InputField").GetComponent<InputField>();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.text = taskDescription;
        field.text = taskSignature;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber = taskNumber;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskSignature = taskSignature;
    }
}
