using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTaskButtonBehaviour : MonoBehaviour
{
    private int tasksCount = 4;

    public void CloseTask()
    {
        GameObject player = GameObject.Find("Snowman");
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
        InputField taskField = GameObject.Find("TaskField").GetComponent<InputField>();
        taskField.text = "";
        taskField.transform.position = taskField.GetComponent<TaskFieldBehaviour>().TurnOffPosition;
        for (var i = 1; i <= tasksCount; i++)
        {
            var task = GameObject.Find("Panel_Task" + i);
            if (task != null)
                task.transform.position = task.GetComponent<PanelTaskBehaviour>().TurnOffPosition;
        }    
    }
}
