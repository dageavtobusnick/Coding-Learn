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
        /*try
        {*/
            robotManagementCode = GetRobotManagementClass();
            ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
            ScriptType type = domain.CompileAndLoadMainSource(robotManagementCode);
            ScriptProxy proxy = type.CreateInstance(robot);
            Tuple<bool, string> result = (Tuple<bool, string>)proxy.Call("isTaskCompleted");
            if (result.Item1)
            {
                UI.ResultField.text = "<color=green>Задание выполнено!</color>";
                Canvas.GetComponent<TaskCompletingActions>().MakeActions(taskNumber);
                UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
            }
            else UI.ResultField.text = "Есть ошибки. Попробуй ещё раз!";
            UI.OutputField.text = result.Item2;
        //}
        /*catch
        {
            Debug.Log("Exception!!!");
            UI.ResultField.text = "Есть ошибки. Попробуй ещё раз!";
        }*/
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        robot = gameData.Player;
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
