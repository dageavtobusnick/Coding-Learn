using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme4ButtonBehaviour : MonoBehaviour
{
    private GameObject mainThemes;
    private GameObject subThemes;

    public void ShowSubThemes()
    {
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        subThemes.transform.position = subThemes.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
    }

    private void Start()
    {
        mainThemes = GameObject.Find("Panel_MainThemes");
        subThemes = GameObject.Find("Panel_Theme4");
    }
}
