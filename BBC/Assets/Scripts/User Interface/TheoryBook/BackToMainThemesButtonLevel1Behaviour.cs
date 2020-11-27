using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainThemesButtonLevel1Behaviour : MonoBehaviour
{
    private GameObject mainThemes;
    private GameObject theme1;

    public void ReturnMainThemes()
    {
        theme1.transform.position = theme1.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
    }

    private void Start()
    {
        mainThemes = GameObject.Find("Panel_MainThemes");
        theme1 = GameObject.Find("Panel_Theme1");
    }
}
