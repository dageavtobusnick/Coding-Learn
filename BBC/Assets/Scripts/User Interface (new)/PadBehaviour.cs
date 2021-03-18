using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadBehaviour : MonoBehaviour
{
    public string startCode;
    private InputField codeField;
    private InputField resultField;
    private bool isPadTurnedOn; 

    public void TurnOnOff()
    {
        if (isPadTurnedOn)
        {
            codeField.text = "";
            resultField.text = "";
            isPadTurnedOn = false;
        }
        else 
        {
            codeField.text = startCode;
            isPadTurnedOn = true;
        }
    }

    public void ResetCode()
    {
        codeField.text = startCode;
    }

    private void Start()
    {
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        isPadTurnedOn = false;
    }
}
