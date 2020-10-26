using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

public class RunButtonBehavior : MonoBehaviour
{
    public void ExecuteCode()
    {
        InputField field = GameObject.Find("InputField").GetComponent<InputField>();
        Text resultField = GameObject.Find("ResultField").GetComponent<Text>();
        var results = GetCompiledAssembly(field);
        if (results.Errors.Count == 0)
        {
            var taskParams = GetTaskParams(field);
            var isTaskCompleted = true;
            var instance = results.CompiledAssembly.CreateInstance("YourSolution.YourClass");
            for (var i = 0; i < taskParams.Item1.Count; i++)
            {
                var answer = results.CompiledAssembly.GetType("YourSolution.YourClass").GetMethod("YourMethod").Invoke(instance, taskParams.Item1[i]);
                if (!answer.Equals(taskParams.Item2[i]))
                {
                    isTaskCompleted = false;
                    break;
                }
            }
            if (isTaskCompleted)
                resultField.text = "Task completed! You're breathtaking!";
            else resultField.text = "Wrong answer! Try again!";
        }
        else
        {
            resultField.text = "";
            foreach (var error in results.Errors)
                resultField.text += error.ToString() + "\n";
        }
    }

    private Tuple<List<object[]>, List<object>> ReturnTask1Params(List<object[]> methodParams, List<object> expectedResults)
    {
        methodParams.Add(new object[] { 12, 32 });
        expectedResults.Add(Tuple.Create(44, 384));
        return Tuple.Create(methodParams, expectedResults);
    }

    private Tuple<List<object[]>, List<object>> ReturnTask2Params(List<object[]> methodParams, List<object> expectedResults)
    {
        methodParams.Add(new object[] { 12, 32, 7 });
        expectedResults.Add(12);
        return Tuple.Create(methodParams, expectedResults);
    }

    private Tuple<List<object[]>, List<object>> ReturnTask3Params(List<object[]> methodParams, List<object> expectedResults)
    {
        methodParams.Add(new object[] { 3, 4, 4 });
        expectedResults.Add(Tuple.Create(9, 16, 16));
        methodParams.Add(new object[] { 7, 5, 3 });
        expectedResults.Add(Tuple.Create(7, 7, 7));
        methodParams.Add(new object[] { 12, 32, -7 });
        expectedResults.Add(Tuple.Create(-12, -32, 7));
        return Tuple.Create(methodParams, expectedResults);
    }

    private Tuple<List<object[]>, List<object>> ReturnTask4Params(List<object[]> methodParams, List<object> expectedResults)
    {
        methodParams.Add(new object[] { 10 });
        expectedResults.Add("рублей");
        methodParams.Add(new object[] { 1 });
        expectedResults.Add("рубль");
        methodParams.Add(new object[] { 2 });
        expectedResults.Add("рубля");
        methodParams.Add(new object[] { 11 });
        expectedResults.Add("рублей");
        methodParams.Add(new object[] { 13 });
        expectedResults.Add("рублей");
        methodParams.Add(new object[] { 22 });
        expectedResults.Add("рубля");
        return Tuple.Create(methodParams, expectedResults);
    }

    private Tuple<List<object[]>, List<object>> GetTaskParams(InputField field)
    {
        var methodParams = new List<object[]>();
        var expectedResults = new List<object>();
        var taskNumber = field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber;
        switch(taskNumber)
        {
            case 1:
                return ReturnTask1Params(methodParams, expectedResults);
            case 2:
                return ReturnTask2Params(methodParams, expectedResults);
            case 3:
                return ReturnTask3Params(methodParams, expectedResults);
            case 4:
                return ReturnTask4Params(methodParams, expectedResults);
            default:
                return null;
        }
    }

    private CompilerResults GetCompiledAssembly(InputField field)
    {
        var provider = new CSharpCodeProvider();
        var parameters = new CompilerParameters();
        parameters.GenerateExecutable = false;
        parameters.GenerateInMemory = true;
        return provider.CompileAssemblyFromSource(parameters, field.text);
    }
}

