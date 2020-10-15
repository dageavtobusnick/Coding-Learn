using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonBehaviour : MonoBehaviour
{
    GameObject player;
    InputField taskField;
    InputField field;
    int taskNumber = 1;
    string taskDescription = "Задача проста - получи сумму двух чисел!";
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
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        player = GameObject.Find("Snowman");
        field = GameObject.Find("InputField").GetComponent<InputField>();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.gameObject.SetActive(true);
        taskField.text = taskDescription;
        field.text = taskSignature;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber = taskNumber;
        field.gameObject.GetComponent<InputFieldBehaviour>().taskSignature = taskSignature;
    }
}
