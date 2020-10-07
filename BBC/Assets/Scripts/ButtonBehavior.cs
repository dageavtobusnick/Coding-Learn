using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.CodeDom;

public class ButtonBehavior : MonoBehaviour
{
    public void ExecuteCode()
    {
        InputField field = GameObject.FindWithTag("Code_InputField").GetComponent<InputField>();
        var provider = new CSharpCodeProvider();
        var parameters = new CompilerParameters();
        parameters.GenerateExecutable = false;
        parameters.GenerateInMemory = true;
        var results = provider.CompileAssemblyFromSource(parameters, field.text);
        if (results.Errors.Count == 0)
        {
            var instance = results.CompiledAssembly.CreateInstance("Test.TestClass");
            var answer = (int)results.CompiledAssembly.GetType("Test.TestClass").GetMethod("TestMethod").Invoke(instance, null);
            Debug.Log(answer);
        }
        else
        {
            foreach (var error in results.Errors)
                Debug.Log(error.ToString());
        }
    }
}

