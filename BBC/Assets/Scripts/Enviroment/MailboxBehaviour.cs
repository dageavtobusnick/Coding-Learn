using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailboxBehaviour : MonoBehaviour
{
    GameObject button;
    GameObject taskField;
    Text resultField;
    InputField inputField;

    void Start()
    {
        button = GameObject.Find("TaskButton_2");
        taskField = GameObject.Find("TaskField");
        button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<Text>();
        taskField.GetComponent<Text>().text = "";
        inputField.text = "";
        resultField.text = "";
        button.SetActive(false);
        taskField.SetActive(false);
    }
}
