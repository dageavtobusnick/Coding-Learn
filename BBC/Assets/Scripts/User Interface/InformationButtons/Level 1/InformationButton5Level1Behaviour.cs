using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton5Level1Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
Для того, чтобы определить переменную, используется конструкция 
тип_данных имя_переменной;
Например
int a;//объявили целочисленную переменную а 
Теперь можем использовать ее в программе, но мы не указали какое значение хотим хранить в нашей переменной. Тогда за нас это сделает компилятор и инициализирует переменную a = 0
Инициализация – присвоение значения.
Мы можем изменить значение в переменной a
a = 123;//теперь переменная а содержит в себе число 123
Можно объединить объявление и инициализацию в одну строку
int a = 123;//сразу после определения переменной присвоили ей значение 123";

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
