using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseExchangeStatusPanelButtonBehaviour : MonoBehaviour
{
    public void CloseExchangeStatusPanel()
    {
        InputField exchangeStatusPanel = GameObject.Find("Panel_RubyToTheoryExchangeStatus").GetComponent<InputField>();
        exchangeStatusPanel.text = "";
        exchangeStatusPanel.transform.position = GameObject.Find("UI_Collector").transform.position;
    }
}
