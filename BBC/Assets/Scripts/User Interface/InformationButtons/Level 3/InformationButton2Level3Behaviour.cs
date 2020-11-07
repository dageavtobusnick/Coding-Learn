using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton2Level3Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
    Мы можем соединить сразу несколько условий, используя логические операторы:
int num1 = 8;
int num2 = 6; 
if (num1 > num2 && num1 == 8) 
{ 
    Console.WriteLine($""Число {num1} больше числа { num2 }"");
}
В данном случае блок if будет выполняться, если num1 > num2 равно true и num1==8 равно true.";

    public void ShowInformation()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        theoryField.transform.position = theoryField.GetComponent<TheoryFieldBehaviour>().TurnOnPosition;
        theoryField.text = information;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        theoryField = GameObject.Find("TheoryField").GetComponent<InputField>();
    }
}
