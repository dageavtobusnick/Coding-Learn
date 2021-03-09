using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RoslynCSharp;

public class StartButtonBehaviour : MonoBehaviour
{
    public int taskNumber;
    private InputField codeField;
    private InputField resultField;
    private GameObject robot;
    private Slider progressBar;
    private float progress = 0f;
    private float fillSpeed = 0.3f;

    public void ExecuteCode()
    {
        var robotManagementCode = GetRobotManagementClass();
        ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
        ScriptType type = domain.CompileAndLoadMainSource(robotManagementCode);
        ScriptProxy proxy = type.CreateInstance(robot);
        var isResultCorrect = (bool)proxy.Call("isTaskCompleted_" + taskNumber);    
        if (isResultCorrect)
        {
            resultField.text = "Задание выполнено!";
            progress += 0.333f;
            var taskButton = GameObject.Find("TaskButton_" + taskNumber).GetComponent<Button>();
            taskButton.interactable = false;
            taskButton.image.color = Color.green;
        }
        else resultField.text = "Есть ошибки. Попробуй ещё раз!";
    }

    private void Update()
    {
        if (progressBar.value < progress)
            progressBar.value += fillSpeed * Time.deltaTime;
    }

    private void Start()
    {
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        robot = GameObject.Find("robot1");
        progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();
    }

    private string GetRobotManagementClass()
    {
        return @"
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class RobotManagementClass : MonoBehaviour
{" + codeField.text + @"

    public int movesCount; 
    public int rotatesCount;

    public bool isTaskCompleted_1()
    {
        var result = SolveTask();
        if (result == 5)
        {
            Run(5);
            return true;
        }
        return false;
    }

    public bool isTaskCompleted_2()
    {
        var result = (int)SolveTask();
        if (result == 3)
        {
            Rotate(3);
            return true;
        }
        return false;
    }

    public bool isTaskCompleted_3()
    {
        var result = (int)SolveTask();
        if (result == 6)
        {
            StartCoroutine(task3_COR());
            return true;
        }
        return false;
    }

    private void Run(int requiredMovesCount)
    {
        movesCount = requiredMovesCount;
        StartCoroutine(Run_COR());
    }

    private IEnumerator Run_COR()
    {
        for (var i = 0; i < movesCount; i++)
        {
            gameObject.transform.position += new Vector3(0.0f, 0.0f, -0.3f);
            yield return new WaitForSeconds(1.2f);
        }
    }

    private void Rotate(int requiredRotatesCount)
    {
        rotatesCount = requiredRotatesCount;
        StartCoroutine(Rotate_COR());
    }

    private IEnumerator Rotate_COR()
    {
        for (var i = 0; i < rotatesCount; i++)
        {
            gameObject.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
            yield return new WaitForSeconds(1.2f);
        }
    }

    private IEnumerator task3_COR()
    {
        Rotate(1);
        yield return new WaitForSeconds(1.2f);
        Run(2);
        yield return new WaitForSeconds(1.2f);
        Rotate(1);
        yield return new WaitForSeconds(1.2f);
        Run(2);
    }
}";
    }
}
