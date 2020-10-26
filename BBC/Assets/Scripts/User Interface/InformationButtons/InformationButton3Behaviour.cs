using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButton3Behaviour : MonoBehaviour
{
    GameObject player;
    InputField taskField;
    string information = "Часть теории 3";

    public void ShowInformation()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOnPosition;
        taskField.text = information;
    }

    private void Start()
    {
        player = GameObject.Find("Snowman");
        taskField = GameObject.Find("TaskField").GetComponent<InputField>();
    }
}
