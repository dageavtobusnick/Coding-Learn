using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoryButtonBehaviour : MonoBehaviour
{
    private GameObject player;
    private GameObject theoryBook;
    private GameObject mainThemes;
    
    public void OpenTheoryBook()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        theoryBook.transform.position = theoryBook.GetComponent<TheoryBookBehaviour>().TurnOnPosition;
        mainThemes.transform.position = mainThemes.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
    }
    
    void Start()
    {
        player = GameObject.Find("Snowman");
        theoryBook = GameObject.Find("Panel_TheoryBook");
        mainThemes = GameObject.Find("Panel_MainThemes");
    } 
}

