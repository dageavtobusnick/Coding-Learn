using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton1Level1Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"                                                              Первое знакомство
    C# - объектно-ориентированный язык программирования, который позволяет создавать разнообразные защищенные и надежные программы.
Код состоит из инструкций. Принято, что инструкция – это одна строка, в конце которой нужно ставить “;” для того, чтобы показать, что инструкция закончилась.
Console.WriteLine(“Добро пожаловать в C#”);
Данная строка представляет вызов метода, который напечатает на консоль строку, заключенную в кавычках.
Набор инструкций можно определить в блок кода. Блок кода заключается в фигурные скобки {}
Выглядит следующим образом:
{
	Console.WriteLine(“Добро пожаловать в C#”);			
	Console.WriteLine(“Добро пожаловать в C#”);			
}										
В блоке кода 2 инструкции, которые выводят на консоль определенную строку
Одни блоки кода могут содержать другие блоки
{
	Console.WriteLine(“Добро пожаловать в C#”);
	{
    		Console.WriteLine(“Добро пожаловать в C#”);	
	}
}";


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
