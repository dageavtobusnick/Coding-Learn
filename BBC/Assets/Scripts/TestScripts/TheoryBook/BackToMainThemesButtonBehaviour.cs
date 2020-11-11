using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainThemesButtonBehaviour : MonoBehaviour
{
    private GameObject mainThemes;
    private GameObject theme1;
    private GameObject theme3;
    private GameObject theme4;

    public void ReturnMainThemes()
    {
        theme1.transform.position = theme1.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        theme3.transform.position = theme3.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        theme4.transform.position = theme4.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
    }

    private void Start()
    {
        mainThemes = GameObject.Find("Panel_MainThemes");
        theme1 = GameObject.Find("Panel_Theme1");
        theme3 = GameObject.Find("Panel_Theme3");
        theme4 = GameObject.Find("Panel_Theme4");
    }
}
