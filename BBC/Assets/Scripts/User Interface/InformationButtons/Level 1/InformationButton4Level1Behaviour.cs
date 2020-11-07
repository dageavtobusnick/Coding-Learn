using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton4Level1Behaviour : MonoBehaviour
{
    GameObject player;
    InputField theoryField;
    string information = @"                                                             Переменные
    Для хранения данных в программе используется переменные. Переменная – именованная область памяти, в которой хранится значение определенного типа
В С# есть своя система типов данных.
У переменной есть тип и значение
Тип – определяет внутреннее значение переменной(целое число, дробное число, текст и т.д.)
Значение – определяет содержимое переменной(конкретное число или строка и т.д.)
Перед использованием переменной ее нужно объявить
тип имя_переменной;
Имя может содержать любые цифры, буквы и символ подчеркивания, при этом первый символ в имени должен быть буквой или символом подчеркивания
В имени не должно быть знаков пунктуации и пробелов
Имя не может быть ключевым словом языка C#.
Имя может быть любым, но следует давать осмысленные имена, чтобы любой, кто захочет прочитать код, мог понять для чего именно эта переменная объявлена.

int: хранит целое число от и занимает 4 байта (int a);
double: хранит дробное число с плавающей точкой и занимает 8 байт (double d);
char: хранит одиночный символ в кодировке Unicode и занимает 2 байта (char c);
string: хранит набор символов Unicode (string s);
bool: хранит логическое значение true или false (правда или ложь, 1 или 0) (bool b);";

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
