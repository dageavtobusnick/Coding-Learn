using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTaskButtonBehaviour : MonoBehaviour
{
    public void CloseTask()
    {
        GameObject player = GameObject.Find("Snowman");
        GameObject panel = GameObject.Find("Panel");
        InputField inputField = GameObject.Find("InputField").GetComponent<InputField>();
        InputField taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        Text resultField = GameObject.Find("ResultField").GetComponent<Text>();
        inputField.text = "";
        resultField.text = "";
        taskField.text = "";
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOffPosition;
        panel.transform.position = panel.GetComponent<PanelBehaviour>().TurnOffPosition;
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
        //taskField.gameObject.SetActive(false);
    }
}
