using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonsBehaviour : MonoBehaviour
{
    private InputField taskField;
    private InputField codeField;
    private GameObject mainCamera;

    public void ShowTask_1()
    {
        mainCamera.GetComponent<StartButtonBehaviour>().taskNumber = 1;
        taskField.text = "- Создай переменную stepsCount типа int и присвой ей значение 2.\n" +
                         "- Создай переменную extraStepsCount типа int и присвой ей значение 3.\n" +
                         "- Верни сумму этих переменных.";
        codeField.text = "public int SolveTask()\n" +
                         "{\n" +
                         "    \n" +
                         "    \n" +
                         "    \n" +
                         "}\n";
    }

    public void ShowTask_2()
    {
        mainCamera.GetComponent<StartButtonBehaviour>().taskNumber = 2;
        taskField.text = "- Создай переменную strangeNumber типа double и присвой ей значение 1.5.\n" +
                         "- Создай переменную anotherStrangeNumber типа double и присвой ей значение 2.0.\n" +
                         "- Верни произведение этих переменных.";
        codeField.text = "public double SolveTask()\n" +
                         "{\n" +
                         "    \n" +
                         "    \n" +
                         "    \n" +
                         "}\n";
    }

    public void ShowTask_3()
    {
        mainCamera.GetComponent<StartButtonBehaviour>().taskNumber = 3;
        taskField.text = "- Создай переменную stepsCount типа int и присвой ей значение 2.\n" +
                         "- Создай переменную rotatesCount типа double и присвой ей значение 1.0.\n" +
                         "- Верни сумму этих переменных, умноженную на 2.";
        codeField.text = "public double SolveTask()\n" +
                         "{\n" +
                         "    \n" +
                         "    \n" +
                         "    \n" +
                         "}\n";
    }

    private void Start()
    {
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        mainCamera = GameObject.Find("Main Camera");
    }
}
