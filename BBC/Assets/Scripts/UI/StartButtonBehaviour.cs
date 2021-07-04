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
            robotManagementCode = GetRobotManagementClass(GetTaskExtraCode());
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
       gameData.Tests[taskNumber - 1].Code + @"
       var output = totalResult ? ""корректный"" : ""неправильный"";
       return Tuple.Create(totalResult, ""Выход: "" + output);
   }
}";
    }

    private string GetTaskExtraCode()
    {
        switch (gameData.SceneIndex)
        {
            case 3:
                switch (taskNumber)
                {
                    case 1:
                        return "public int L3_T1_boxesCount = 0;" +
                               "public void OpenContainer() => L3_T1_boxesCount++;";
                    case 2:
                        return "public int L3_T2_isAxeCallsCount = 0;" +
                               "public int L3_T2_pickUpCallsCount = 0;" +
                               "public bool IsAxe()" +
                               "{" +
                               "    L3_T2_isAxeCallsCount++;" +
                               "    return L3_T2_isAxeCallsCount == 5;" +
                               "}" +
                               "public bool IsSaw() => false;" +
                               "public void PickUp() => L3_T2_pickUpCallsCount++;";
                    case 3:
                        return "public int L3_T3_treesCount = 10;" +
                               "public void UseAxe() => L3_T3_treesCount--;" +
                               "public bool IsPathClear() => L3_T3_treesCount == 0;";
                    case 4:
                        return "public int L3_T4_isTreeTallCallsCount = 0;" +
                               "public int L3_T4_useAxeCallsCount = 0;" +
                               "public void UseAxe() => L3_T4_useAxeCallsCount++;" +
                               "public bool IsTreeTall()" +
                               "{" +
                               "    L3_T4_isTreeTallCallsCount++;" +
                               "    return L3_T4_isTreeTallCallsCount % 2 == 1;" +
                               "}";
                    case 5:
                        return "public int L3_T5_setUpBoardCallsCount = 0;" +
                               "public void SetUpBoard() => L3_T5_setUpBoardCallsCount++;";
                    case 6:
                        return "public int L3_T6_chooseNewPlaceCallsCount = 0;" +
                               "public int L3_T6_searchKeyCallsCount = 0;" +
                               "public void ChooseNewPlace() => L3_T6_chooseNewPlaceCallsCount++;" +
                               "public void SearchKey() => L3_T6_searchKeyCallsCount++;" +
                               "public bool IsKeyFound() => L3_T6_chooseNewPlaceCallsCount == 3 && L3_T6_searchKeyCallsCount == 3;";
                    case 7:
                        return "public int L3_T7_checkNewItemsCallsCount = 0;" +
                               "public void CheckNewItem() => L3_T7_checkNewItemsCallsCount++;" +
                               "public bool IsKeyFound() => L3_T7_checkNewItemsCallsCount == 12;";
                    case 8:
                        return "public int L3_T8_turnToNextTargetCallsCount = 0;" +
                               "public int L3_T8_digsCount = 0;" +
                               "public void TurnToNextTarget() => L3_T8_turnToNextTargetCallsCount++;" +
                               "public void Dig(int repeatsCount) => L3_T8_digsCount += repeatsCount;" +
                               "public bool IsKeyFound() => L3_T8_digsCount == 9;";
                }
                break;
        }
        return null;
    }
}
