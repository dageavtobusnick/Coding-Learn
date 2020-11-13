using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton1Level3Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
    Отдельный набор операций представляет условные выражения. Такие операции возвращают логическое значение. 
    Логическое значение — это переменная, которая может иметь одно из двух значений: true или false. Логические переменные в C# определяются особым типом — bool. 
    Оператор if проверяет значение bool. Если значение true, выполняется оператор, следующий после if. В противном случае он пропускается. 
    Условные конструкции - один из базовых компонентов многих языков программирования, которые направляют работу программы по одному из путей в зависимости от определенных условий.";

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
