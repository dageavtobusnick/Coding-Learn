using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPageButtonBehaviour : MonoBehaviour
{
    private GameObject page1;
    private GameObject page2;

    public void ShowPage_1()
    {
        page1.transform.position = page1.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
        page2.transform.position = page2.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
    }

    public void ShowPage_2()
    {
        page2.transform.position = page2.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
        page1.transform.position = page1.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
    }

    private void Start()
    {
        page1 = GameObject.Find("Panel_Training_1"); 
        page2 = GameObject.Find("Panel_Training_2");
    }
}
