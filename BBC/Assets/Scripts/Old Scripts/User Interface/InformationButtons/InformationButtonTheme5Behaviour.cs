using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButtonTheme5Behaviour : MonoBehaviour
{
    private GameObject player;
    private InputField theoryField;
    private int themeNumber = 5;
    private int partNumber;

    public void ShowTheory_Part1()
    {
        partNumber = 1;
        ShowInformation();
    }

    public void ShowTheory_Part2()
    {
        partNumber = 2;
        ShowInformation();
    }

    public void ShowTheory_Part3()
    {
        partNumber = 3;
        ShowInformation();
    }

    public void ShowTheory_Part4()
    {
        partNumber = 4;
        ShowInformation();
    }

    public void ShowTheory_Part5()
    {
        partNumber = 5;
        ShowInformation();
    }

    public void ShowTheory_Part6()
    {
        partNumber = 6;
        ShowInformation();
    }

    private void ShowInformation()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        theoryField.transform.position = theoryField.GetComponent<TheoryFieldBehaviour>().TurnOnPosition;
        GameObject.Find("TheoryImage").GetComponent<TheoryImageBehaviour>().themeNumber = themeNumber;
        GameObject.Find("TheoryImage").GetComponent<TheoryImageBehaviour>().imageNumber = partNumber - 1;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        theoryField = GameObject.Find("TheoryField").GetComponent<InputField>();
    }
}
