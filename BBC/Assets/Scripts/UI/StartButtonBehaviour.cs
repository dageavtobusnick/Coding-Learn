using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RoslynCSharp;

public class StartButtonBehaviour : MonoBehaviour
{
    [Header ("Номер текущего задания")]
    public int taskNumber;
    [Header("Интерфейс")]
    public GameObject Canvas;

    private GameObject robot;
    private InterfaceElements UI;
    private GameData gameData;
    private string robotManagementCode;

    public void ExecuteCode()
    {
        try
        {
            robotManagementCode = GetRobotManagementClass(gameData.Tests[taskNumber - 1].ExtraCode);
            ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
            ScriptType type = domain.CompileAndLoadMainSource(robotManagementCode);
            ScriptProxy proxy = type.CreateInstance(robot);
            StartCoroutine(ShowExecutingProcess(proxy));      
        }
        catch
        {
            Debug.Log("Exception!!!");
            UI.ResultField.text = "Есть ошибки. Попробуй ещё раз!";
        }
    }

    private IEnumerator ShowExecutingProcess(ScriptProxy proxy)
    {
        for (var i = 0; i <= 3; i++)
        {
            UI.OutputField.text = "Выполнение" + new string('.', i);
            yield return new WaitForSeconds(0.5f);
        }
        Tuple<bool, string> result = (Tuple<bool, string>)proxy.Call("isTaskCompleted");
        if (result.Item1)
        {
            UI.ResultField.text = "<color=green>Задание выполнено!</color>";
            gameData.TasksScores++;
            Canvas.GetComponent<TaskCompletingActions>().MakeActions(taskNumber);
            UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        }
        else UI.ResultField.text = "Есть ошибки. Попробуй ещё раз!";
        UI.OutputField.text = result.Item2;
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        robot = gameData.Player;
        LaunchCompiler();
    }

    private void LaunchCompiler()
    {
        ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
        ScriptType type = domain.CompileAndLoadMainSource(@"
using UnityEngine;
using System;

public class LaunchClass : MonoBehaviour
{
    public void LaunchCompiler() => Debug.Log(""Compiler is working!"");
}");
        ScriptProxy proxy = type.CreateInstance(robot);
        proxy.Call("LaunchCompiler");
    }

    private string GetRobotManagementClass(string extraCode)
    {
        return @"
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RobotManagementClass : MonoBehaviour
{" + extraCode + 
   UI.CodeField.text + @"
   public Tuple<bool, string> isTaskCompleted()
   {" +
       gameData.Tests[taskNumber - 1].TestCode + @"
       var output = totalResult ? ""корректный"" : ""неправильный"";
       return Tuple.Create(totalResult, ""Выход: "" + output);
   }
}";
    }
}
