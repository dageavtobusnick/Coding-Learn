using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainThemesButtonLevel3Behaviour : MonoBehaviour
{
    private GameObject mainThemes;
    private GameObject theme1;
    private GameObject theme3;

    public void ReturnMainThemes()
    {
        theme1.transform.position = theme1.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        theme3.transform.position = theme3.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
    }

    private void Start()
    {
        mainThemes = GameObject.Find("Panel_MainThemes");
        theme1 = GameObject.Find("Panel_Theme1");
        theme3 = GameObject.Find("Panel_Theme3");
    }
}
