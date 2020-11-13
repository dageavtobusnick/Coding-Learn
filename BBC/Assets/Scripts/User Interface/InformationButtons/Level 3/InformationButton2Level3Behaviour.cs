using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton2Level3Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
    В языке C# используются следующие условные конструкции: if..else. Конструкция if/else проверяет истинность некоторого условия и в зависимости от результатов проверки выполняет определенный код: 
int num1 = 8; 
int num2 = 6; 
if(num1 > num2) 
{ 
    Console.WriteLine($""Число {num1} больше числа {num2}""); 
}
После ключевого слова if ставится условие. И если это условие выполняется, то срабатывает код, который помещен далее в блоке if после фигурных скобок. В качестве условий выступают ранее рассмотренные операции сравнения. В данном случае у нас первое число больше второго, поэтому выражение num1>num2 истинно и возвращает true, следовательно, управление переходит к строке Console.WriteLine(""Число {num1} больше числа {num2}"");.";

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
