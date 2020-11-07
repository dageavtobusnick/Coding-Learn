using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton3Level4Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"                                                       Цикл с параметром - for
Рассмотрим тот же пример - поиск факториала числа. Как вы видите, мы заранее знаем, сколько раз  должно повториться тело цикла, потому можем использовать счетчик.
int n = Convert.ToInt32(Console.ReadLine()); // Пользователь вводит число.
int factorial = 1; 
for(int i = 2; i <= n; i++) // Вычисление факториала.
{
    factorial *= i; 
}
Console.WriteLine(factorial); // Выводим факториал пользователю.
Итак, пользователь вводит любое число. После чего, мы вычисляем факториал по вышеуказанной формуле. Начальное значение факториала необходимо установить в единицу. Цикл начинаем с двойки и повторяем до тех пор, пока счетчик меньше или равен введенному пользователем значению. 
Если использовать оператор «меньше», мы потеряем умножение на старшее число при вычислении факториала. Порядок выполнения указан как i++, это значит, что на каждой итерации цикла счетчик i увеличивается на единицу. В виде порядка управления может выступать и более сложная математическая формула.";

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
