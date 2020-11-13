using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton5Level3Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
    При сравнении чисел мы можем насчитать три состояния: первое число больше второго, первое число меньше второго и числа равны. Используя конструкцию else if, мы можем обрабатывать дополнительные условия:
int num1 = 8; 
int num2 = 6; 
if(num1 > num2) 
{ 
    Console.WriteLine($""Число {num1} больше числа { num2 }""); 
} 
else if (num1 < num2) 
{ 
    Console.WriteLine($""Число{ num1 } меньше числа { num2 }""); 
} 
else Console.WriteLine(""Число num1 равно числу num2"")";

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
