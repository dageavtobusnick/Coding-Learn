using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton2Behaviour : MonoBehaviour
{
    public void ShowTask()
    {
        GameObject taskField_GO = GameObject.Find("TaskField");
        taskField_GO.SetActive(true);
        InputField taskField = taskField_GO.GetComponent<InputField>();
        InputField field = GameObject.Find("InputField").GetComponent<InputField>();       
        taskField.text = "Следующая задача сложнее - сложи 3 числа!(это невозможно, даже не пытайся!)";
        field.text = @"
using System;

namespace YourSolution
{
    class YourClass
    {
        public int YourMethod(int a, int b, int c)
        {
        
        }
    }
}";
    }
}
