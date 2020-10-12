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
        //Text taskText1 = GameObject.Find("TaskText").GetComponent<Text>();
        //Text taskText2 = GameObject.Find("TaskText_2").GetComponent<Text>();
        var results = GetCompiledAssembly(field);
        if (results.Errors.Count == 0)
        {
            var taskParams = GetTaskParams();
            var instance = results.CompiledAssembly.CreateInstance("YourSolution.YourClass");
            var answer = results.CompiledAssembly.GetType("YourSolution.YourClass").GetMethod("YourMethod").Invoke(instance, taskParams.Item1);
            if (answer.Equals(taskParams.Item2))
                resultField.text = "That's correct! You're breathtaking!";
            else resultField.text = "That's wrong answer! Try again!";
        }
        else
        {
            resultField.text = "";
            foreach (var error in results.Errors)
                resultField.text += error.ToString() + "\n";
        }
    }

    private Tuple<object[], object> ReturnTask1Params()
    {
        object[] methodParams = { 5, 6 };
        object expectedResult = 11;
        return Tuple.Create(methodParams, expectedResult);
    }

    private Tuple<object[], object> ReturnTask2Params()
    {
        object[] methodParams = { 5, 6, 7 };
        object expectedResult = 18;
        return Tuple.Create(methodParams, expectedResult);
    }

    private Tuple<object[], object> GetTaskParams()
    {
        if (GameObject.Find("TaskText").activeInHierarchy)
            return ReturnTask1Params();
        else if (GameObject.Find("TaskText_2").activeInHierarchy)
            return ReturnTask2Params();
        else throw new Exception();
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

