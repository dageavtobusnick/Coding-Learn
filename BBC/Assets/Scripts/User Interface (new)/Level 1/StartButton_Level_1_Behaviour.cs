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
    private GameObject robot;

    public void ExecuteCode()
    {
        var robotManagementCode = GetRobotManagementClass();
        ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
        try
        {
            ScriptType type = domain.CompileAndLoadMainSource(robotManagementCode);
            ScriptProxy proxy = type.CreateInstance(robot);
            var result = proxy.Call("isTaskCompleted_" + taskNumber);
            if ((bool)result)
            {
                resultField.text = "Команды выполнены!";
                taskNumber++;
            }
            else resultField.text = "Есть ошибки. Попробуй ещё раз!";
        }
        catch 
        {
            resultField.text = "Есть ошибки. Попробуй ещё раз!";
        }     
    }

    private void Start()
    {
        robot = GameObject.Find("robot1");
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        taskNumber = 1;
    }

    private string GetCheckingMethods()
    {
        return @"
public bool isTaskCompleted_1()
{
    var result = Execute();
    if (result == 16)
    {
        StartCoroutine(Run(8));
        return true;
    }
    return false;
}

public bool isTaskCompleted_2()
{
    var result = Execute();
    if (result == 20.5)
    {
        StartCoroutine(Task_2_COR());
        return true;
    }
    return false;
}

public bool isTaskCompleted_3()
{
    var result = Execute();
    if (result == 4 / (1.0 / 3))
    {
        StartCoroutine(Task_3_COR());
        return true;
    }
    return false;
}

private IEnumerator Task_2_COR()
{
    yield return StartCoroutine(Rotate(""Left""));
    yield return StartCoroutine(Run(3));
    yield return StartCoroutine(Rotate(""Right""));
    yield return StartCoroutine(Run(5));
    yield return StartCoroutine(Rotate(""Right""));
}

private IEnumerator Task_3_COR()
{
    direction = Direction.Right;
    yield return StartCoroutine(Run(5));
    yield return StartCoroutine(Rotate(""Left""));
    yield return StartCoroutine(Run(7));
}";
    }

    private string GetRobotManagementClass()
    {
        return @"
using UnityEngine;
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
                vector = new Vector3(0.0f, 0.0f, -2.0f);
                break;
            case Direction.Left:
                vector = new Vector3(2.0f, 0.0f, 0.0f);
                break;
            case Direction.Right:
                vector = new Vector3(-2.0f, 0.0f, 0.0f);
                break;
            case Direction.Back:
                vector = new Vector3(0.0f, 0.0f, 2.0f);
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
