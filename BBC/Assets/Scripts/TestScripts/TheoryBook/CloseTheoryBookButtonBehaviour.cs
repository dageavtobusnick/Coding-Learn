using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTheoryBookButtonBehaviour : MonoBehaviour
{
    public void CloseTheoryBook()
    {
        GameObject player = GameObject.Find("Snowman");
        GameObject theoryBook = GameObject.Find("Panel_TheoryBook");
        GameObject mainThemes = GameObject.Find("Panel_MainThemes");
        GameObject theme1 = GameObject.Find("Panel_Theme1");
        GameObject theme3 = GameObject.Find("Panel_Theme3");
        GameObject theme4 = GameObject.Find("Panel_Theme4");
        theoryBook.transform.position = theoryBook.GetComponent<TheoryBookBehaviour>().TurnOffPosition;
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        theme1.transform.position = theme1.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        theme3.transform.position = theme3.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        theme4.transform.position = theme4.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
    }
}
