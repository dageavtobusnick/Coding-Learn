using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTheoryBookButtonBehaviour : MonoBehaviour
{
    private int themesCount = 5;

    public void CloseTheoryBook()
    {
        GameObject player = GameObject.Find("Snowman");
        GameObject theoryBook = GameObject.Find("Panel_TheoryBook");
        GameObject mainThemes = GameObject.Find("Panel_MainThemes");
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
        theoryBook.transform.position = theoryBook.GetComponent<TheoryBookBehaviour>().TurnOffPosition;   
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        for (var i = 1; i <= themesCount; i++)
        {
            var theme = GameObject.Find("Panel_Theme" + i);
            if (theme != null)
                theme.transform.position = theme.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        }
    }
}
