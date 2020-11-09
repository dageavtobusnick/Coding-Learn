using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton4Level4Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
    Как и условные конструкции, циклы также могут быть вложены друг в друга.
    Вложенные циклы – это циклы, организованные в теле другого цикла. Вложенный цикл в тело другого цикла, называется внутренним циклом. Цикл, в теле которого существует вложенный цикл, называется внешним.
Полное число исполнений внутреннего цикла, всегда равно произведению числа итераций внутреннего цикла на произведение чисел итераций всех внешних циклов, например, если внешний цикл имеет 5 итераций, а внутренний 10, то общее число итераций внутреннего цикла будет 5 * 10 = 50 итераций. 
Пример с вложением одного цикла for в другой цикл for:
for (int i = 0; i < 10; i++)
{
    // Выводим одну строку из 10 звездочек.
    for (int j = 0; j < 10; j++)
    {
        Console.Write("" * "");
    } 
    // Переход на новую строку.
    Console.WriteLine();
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
