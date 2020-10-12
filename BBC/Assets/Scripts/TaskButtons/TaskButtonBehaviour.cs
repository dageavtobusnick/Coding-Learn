using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonBehaviour : MonoBehaviour
{
    public void ShowTask()
    {
        //GameObject panel = GameObject.Find("Panel");
        //panel.SetActive(true);
        Text taskText = GameObject.Find("TaskText").GetComponent<Text>();
        InputField field = GameObject.Find("InputField").GetComponent<InputField>();
        taskText.text = "Задача проста - получи сумму двух чисел!";
        field.text = @"
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
    }
}
