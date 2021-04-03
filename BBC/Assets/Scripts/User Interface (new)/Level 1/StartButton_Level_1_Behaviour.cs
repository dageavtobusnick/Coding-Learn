using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RoslynCSharp;

public class StartButton_Level_1_Behaviour : MonoBehaviour
{
    public int taskNumber;
    private InputField codeField;
    private InputField resultField;
    private InputField outputField;
    private GameObject robot;
    private GameObject canvas;

    public void ExecuteCode()
    {
        try
        {
            var robotManagementCode = GetRobotManagementClass();
            ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
            ScriptType type = domain.CompileAndLoadMainSource(robotManagementCode);
            ScriptProxy proxy = type.CreateInstance(robot);
            Tuple<bool, string> result = (Tuple<bool, string>)proxy.Call("isTaskCompleted_" + taskNumber);
            if (result.Item1)
            {
                resultField.text = "<color=green>Задание выполнено!</color>";
                canvas.GetComponent<TaskPanel_Level_1_Behaviour>().isNextTaskButtonAvailable = true;
            }
            else resultField.text = "Есть ошибки. Попробуй ещё раз!";
            outputField.text = result.Item2;
        }
        catch
        {
            resultField.text = "Есть ошибки. Попробуй ещё раз!";
        }
    }

    private void Start()
    {
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        outputField = GameObject.Find("OutputField").GetComponent<InputField>();
        robot = GameObject.Find("robot1");
        canvas = GameObject.Find("Canvas");
    }

    private string GetCheckingMethods()
    {
        return @"
public Tuple<bool, string> isTaskCompleted_1()
{
    var result = Execute();
    return Tuple.Create(result == 6, ""Выход:  "" + result);
}

public Tuple<bool, string> isTaskCompleted_2()
{
    var result = Execute() * 10000;
    return Tuple.Create(Math.Abs(result - 0.16755) < 1e-4, ""Выход:  "" + result);
}

public Tuple<bool, string> isTaskCompleted_3()
{
    var result = Execute();
    return Tuple.Create(result == 1, ""Выход:  "" + result);
}

public Tuple<bool, string> isTaskCompleted_4()
{
    var result = Execute();
    return Tuple.Create(result == 300, ""Выход:  "" + result);
}";
    }

    private string GetRobotManagementClass()
    {
        return @"
using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;

public class RobotManagementClass : MonoBehaviour
{" + codeField.text + @"

    public enum Direction
    {
        Forward,
        Right,
        Back,
        Left,
    }

    public int movesCount;
    public Direction direction = Direction.Forward;
    private float latency = 0.8f;
    private Animator animator;

    private IEnumerator Run(int requiredMovesCount)
    {
        Vector3 vector = new Vector3();
        movesCount = requiredMovesCount;
        switch (direction)
        {
            case Direction.Forward:
                vector = new Vector3(0.0f, 0.0f, -1.0f);
                break;
            case Direction.Left:
                vector = new Vector3(1.0f, 0.0f, 0.0f);
                break;
            case Direction.Right:
                vector = new Vector3(-1.0f, 0.0f, 0.0f);
                break;
            case Direction.Back:
                vector = new Vector3(0.0f, 0.0f, 1.0f);
                break;
        }
        yield return StartCoroutine(Run_COR(vector));;
    }

    private IEnumerator Run_COR(Vector3 vector)
    {
        for (var i = 0; i < movesCount; i++)
        {
            gameObject.transform.position += vector;
            yield return new WaitForSeconds(latency);
        }           
    }

    private IEnumerator Rotate(string input)
    {
        Vector3 rotationVector;
        if (input == ""Right"")
        {
            rotationVector = new Vector3(0.0f, 90.0f, 0.0f);
            direction = (Direction)(((int)direction + 1) % 4);
        }
        else if (input == ""Left"")
        {
            rotationVector = new Vector3(0.0f, -90.0f, 0.0f);
            direction = (Direction)(((int)direction + 3) % 4);
        }
        else yield break;
        yield return StartCoroutine(Rotate_COR(rotationVector));
    }

    private IEnumerator Rotate_COR(Vector3 vector)
    {
        gameObject.transform.Rotate(vector, Space.Self);
        yield return new WaitForSeconds(latency);
    }" + GetCheckingMethods() + @"
}";
    }
}
