using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingButtonBehaviour : MonoBehaviour
{
    public void ShowTraining()
    {
        var page1 = GameObject.Find("Panel_Training_1");
        page1.transform.position = page1.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
        GameObject.Find("Snowman").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }
}
