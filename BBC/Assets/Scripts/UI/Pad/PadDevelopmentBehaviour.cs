using RoslynCSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PadDevelopmentBehaviour : MonoBehaviour
{
    [Header("������� (����� ����������)")]
    [Tooltip("�������")]
    public GameObject Pad;
    [Tooltip("���� ��� ����� ����")]
    public InputField CodeField;
    [Tooltip("���� ��� ������ ���������� ���������� �������")]
    public Text ResultField;
    [Tooltip("���� ��� ������ ������ ��������� (���������� ��� ���)")]
    public Text OutputField;
    [Tooltip("������ ������� ���������")]
    public Button StartButton;
    [Tooltip("������ ������ ���� � ���������� ���������")]
    public Button ResetButton;
    [Tooltip("������ ��������� ������ ���������")]
    public Button TipButton;
    [Tooltip("������ �������� � ���� �� ������ ����������")]
    public Button ExitDevModeButton;

    [Space] public UnityEvent OnTaskCompleted;

    [HideInInspector] public string StartCode;

    private GameManager gameManager;
    private UIManager uiManager;
    private GameObject player;
    private string robotManagementCode;

    public void SwitchToDevMode() => StartCoroutine(SwitchToDevMode_COR());

    public void ReturnToMenuFromDevMode() => StartCoroutine(ReturnToMenuFromDevMode_COR());

    public void ResetCode() => CodeField.text = StartCode;

    public void ShowNewTaskCode()
    {
        var taskText = gameManager.TaskTexts[gameManager.CurrentTaskNumber - 1];
        StartCode = taskText.StartCode;
        CodeField.text = taskText.StartCode;
        ResultField.text = "";
        OutputField.text = "";
    }

    public void ExecuteCode()
    {
        try
        {
            robotManagementCode = GetRobotManagementClass(gameManager.GetTests().ExtraCode);
            ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
            ScriptType type = domain.CompileAndLoadMainSource(robotManagementCode);
            ScriptProxy proxy = type.CreateInstance(player);
            StartCoroutine(ShowExecutingProcess(proxy));
        }
        catch
        {
            Debug.Log("Runtime code could not compile!");
            ResultField.text = "���� ������. �������� ��� ���!";
        }
    }

    private IEnumerator ShowExecutingProcess(ScriptProxy proxy)
    {
        for (var i = 0; i <= 3; i++)
        {
            OutputField.text = "����������" + new string('.', i);
            yield return new WaitForSeconds(0.5f);
        }
        Tuple<bool, string> result = (Tuple<bool, string>)proxy.Call("isTaskCompleted");
        if (result.Item1)
        {
            ResultField.text = "<color=green>������� ���������!</color>";
            OnTaskCompleted.Invoke();
        }
        else ResultField.text = "���� ������. �������� ��� ���!";
        OutputField.text = result.Item2;
    }

    private IEnumerator SwitchToDevMode_COR()
    {
        Pad.GetComponentInParent<Animator>().Play("SwitchToDevMode");
        uiManager.PadMode = PadMode.Development;
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator ReturnToMenuFromDevMode_COR()
    {
        Pad.GetComponentInParent<Animator>().Play("ReturnToMenuFromDevMode");
        yield return new WaitForSeconds(1f);
        //HelpPanel.GetComponent<Animator>().Play("ScaleDown_Quick");
        uiManager.PadMode = PadMode.Normal;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        player = gameManager.Player;
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
        ScriptProxy proxy = type.CreateInstance(player);
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
   CodeField.text + @"
   public Tuple<bool, string> isTaskCompleted()
   {" +
       gameManager.GetTests().TestCode + @"
       var output = totalResult ? ""����������"" : ""������������"";
       return Tuple.Create(totalResult, ""�����: "" + output);
   }
}";
    }
}
