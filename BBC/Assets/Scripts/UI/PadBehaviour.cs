using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadBehaviour : MonoBehaviour
{
    public enum PadMode
    {
        Normal,
        Development,
        Handbook
    }

    [Header("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public string StartCode;
    [HideInInspector]
    public PadMode Mode;

    private InterfaceElements UI;
    private bool isPadTurnedOff; 

    public void TurnOnOff()
    {
        if (isPadTurnedOff)
        {
            UI.CodeField.text = "";
            UI.ResultField.text = "";
            UI.OutputField.text = "";
            isPadTurnedOff = false;
        }
        else 
        {
            UI.CodeField.text = StartCode;
            isPadTurnedOff = true;
        }
    }

    public void ResetCode() => UI.CodeField.text = StartCode;

    public void SwitchToDevMode()
    {
        UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("SwitchToDevMode");
        Mode = PadMode.Development;
    }

    public void ReturnToMenuFromDevMode()
    {
        UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("ReturnToMenuFromDevMode");
        Mode = PadMode.Normal;
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        isPadTurnedOff = false;
        Mode = PadMode.Normal;
    }
}
