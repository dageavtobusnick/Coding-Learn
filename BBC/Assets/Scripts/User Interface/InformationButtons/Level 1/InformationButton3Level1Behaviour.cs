using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton3Level1Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"
    C# является регистрозависимым языком. Это значит, в зависимости от регистра символов какое-то определенные названия может представлять разные классы, методы, переменные и т.д. Например, название обязательного метода Main начинается именно с большой буквы: ""Main"". 
Если мы назовем метод ""main"", то программа не скомпилируется, так как метод, который представляет стартовую точку в приложении, обязательно должен называться ""Main"", а не ""main"" или ""MAIN"".
    Комментарии нужны чтобы сделать код понятнее. Есть два вида комментариев: однострочный и многострочный.
Однострочный комментарий размещается на одной строке после двойного слеша //
Многострочный комментарий заключается между символами /*комментарий*/
{
        Console.WriteLine()//однострочный комментарий
	/*многострочный комментарий
	Многострочный комментарий
	Многострочный комментарий*/
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
