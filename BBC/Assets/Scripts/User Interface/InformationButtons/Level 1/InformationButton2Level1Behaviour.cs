using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton2Level1Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
Точкой входа в программу является метод Main. При создании проекта создается следующий метод Main:
using System;
namespace HelloApp
{
    class Program
    {
        static void Main(string[] args)
        {
		//Здесь помещаются инструкции
        }
    }
}
Пока что опустим весь код, что находится за пределами метода Main, позже мы его разберем. Как только программа запустится, она начнется с выполнения метода Main. 
По умолчанию метод Main расположен внутри класса Program. Название может быть любым, но метод Main является обязательной частью консольного приложения.";



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
