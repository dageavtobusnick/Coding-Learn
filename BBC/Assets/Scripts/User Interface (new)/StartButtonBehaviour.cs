using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RoslynCSharp;

public class StartButtonBehaviour : MonoBehaviour
{
    public void ExecuteCode()
    {
        InputField codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        InputField resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        ScriptDomain domain = ScriptDomain.CreateDomain("MyDomain");
        ScriptType type = domain.CompileAndLoadMainSource(codeField.text);
        ScriptProxy proxy = type.CreateInstance(GameObject.Find("robot1"));
        var result = proxy.Call("MyMethod");
        resultField.text = result == null ? "Нет вывода" : result.ToString();
    }
}
