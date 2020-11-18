using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeButtonBehaviour : MonoBehaviour
{
    private GameObject mainThemes;
    private GameObject subThemes;

    public void ShowSubThemes_Theme1()
    {
        subThemes = GameObject.Find("Panel_Theme1");
        ShowSubThemes();
    }

    public void ShowSubThemes_Theme3()
    {
        subThemes = GameObject.Find("Panel_Theme3");
        ShowSubThemes();
    }

    public void ShowSubThemes_Theme4()
    {
        subThemes = GameObject.Find("Panel_Theme4");
        ShowSubThemes();
    }

    private void ShowSubThemes()
    {
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        subThemes.transform.position = subThemes.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
    }

    private void Start()
    {
        mainThemes = GameObject.Find("Panel_MainThemes");     
    }
}
