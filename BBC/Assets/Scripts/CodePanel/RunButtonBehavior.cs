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
        object[] methodParams = { 9, 3 };
        object expectedResult = 24;
        return Tuple.Create(methodParams, expectedResult);
    }

    private Tuple<object[], object> GetTaskParams(InputField field)
    {
        if (field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber == 1)
            return ReturnTask1Params();
        else if (field.gameObject.GetComponent<InputFieldBehaviour>().taskNumber == 2)
            return ReturnTask2Params();
        else return null;
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

