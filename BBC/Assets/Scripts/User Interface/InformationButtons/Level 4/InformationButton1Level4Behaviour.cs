using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton1Level4Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
Что мы делаем ежедневно? Думаю, у каждого из нас свой список дел. Однако раз за разом повторяются одни и те же операции для достижения одних и тех же целей. Это и есть цикл. 
Циклы являются управляющими конструкциями, позволяя в зависимости от определенных условий выполнять некоторое действие множество раз.В программировании циклы используются при обработке множеств / файлов или же для вычисления математических выражений. 
В C# имеются следующие виды циклов:for, foreach, while, do...while.";

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
