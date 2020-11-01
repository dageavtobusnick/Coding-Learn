using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTaskButtonBehaviour : MonoBehaviour
{
    public void CloseTask()
    {
        GameObject player = GameObject.Find("Snowman");
        InputField taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        GameObject panelTask1 = GameObject.Find("Panel_Task1");
        GameObject panelTask2 = GameObject.Find("Panel_Task2");
        GameObject panelTask3 = GameObject.Find("Panel_Task3");
        GameObject panelTask4 = GameObject.Find("Panel_Task4");
        taskField.text = "";
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOffPosition;
        panelTask1.transform.position = panelTask1.GetComponent<PanelTask1Behaviour>().TurnOffPosition;
        panelTask2.transform.position = panelTask2.GetComponent<PanelTask2Behaviour>().TurnOffPosition;
        panelTask3.transform.position = panelTask3.GetComponent<PanelTask3Behaviour>().TurnOffPosition;
        panelTask4.transform.position = panelTask4.GetComponent<PanelTask4Behaviour>().TurnOffPosition;
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
    }
}
