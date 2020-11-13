using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton4Level3Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
    Но что, если мы захотим, чтобы при несоблюдении условия также выполнялись какие-либо действия? В этом случае мы можем добавить блок else. Оператор после ключевого слова else выполняется, только если проверяемое условие имеет значение false. 
Объединив операторы if и else с логическими условиями, вы получите все необходимые возможности для обработки условий true и false.
int num1 = 8;
int num2 = 6; 
if(num1 > num2) 
{ 
    Console.WriteLine($""Число {num1} больше числа { num2 }"");
} 
else
{
    Console.WriteLine($""Число { num1 } меньше числа { num2 }"");
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
