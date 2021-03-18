using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainThemesButtonBehaviour : MonoBehaviour
{
    private int themesCount = 5;

    public void ReturnMainThemes()
    {
        var mainThemes = GameObject.Find("Panel_MainThemes");
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
        for (var i = 1; i <= themesCount; i++)
        {
            var theme = GameObject.Find("Panel_Theme" + i);
            if (theme != null)
                theme.transform.position = theme.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        }
    }
}
