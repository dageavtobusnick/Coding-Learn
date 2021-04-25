using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
            robotManagementCode = GetRobotManagementClass();
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

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        robot = gameData.Player;
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
            Canvas.GetComponent<TaskCompletingActions>().MakeActions(taskNumber);
            UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        }
        else UI.ResultField.text = "Есть ошибки. Попробуй ещё раз!";
        UI.OutputField.text = result.Item2;
    }

    private string GetRobotManagementClass()
    {
        return @"
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RobotManagementClass : MonoBehaviour
{" +
   UI.CodeField.text + @"
   public Tuple<bool, string> isTaskCompleted()
   {" +
       gameData.Tests[taskNumber - 1].Code + @"
       var output = totalResult ? ""корректный"" : ""неправильный"";
       return Tuple.Create(totalResult, ""Выход: "" + output);
   }
}";
    }
}
