using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton2Level4Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"                                                     Цикл с предусловием - while
Действия, выполняемые циклически, называются телом цикла. В данном случае действия цикла повторяются до тех пор, пока выполняется указанное условие. Этот цикл функционирует по принципу: «Сперва думаем, после делаем». В общем виде выглядит так:
while(<Условие>)
{
    <Действия>
}
Рассмотрим пример вычисления факториала при помощи while.
int n = Convert.ToInt32(Console.ReadLine()); // Пользователь вводит число.
int factorial = 1; 
int i = 2; 
while(i <= n) // Вычисление факториала.
{
    factorial *= i; 
    i++; // Увеличиваем счетчик.
}

Console.WriteLine(factorial); // Выводим факториал пользователю.
Чтобы не получить бесконечного цикла, необходимо изменять параметр, проверяемый в условии. Именно для этого мы увеличиваем i.";

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
