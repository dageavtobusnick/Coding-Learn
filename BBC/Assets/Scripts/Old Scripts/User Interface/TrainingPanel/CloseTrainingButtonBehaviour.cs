using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTrainingButtonBehaviour : MonoBehaviour
{
    public void CloseTraining()
    {
        var page1 = GameObject.Find("Panel_Training_1");
        var page2 = GameObject.Find("Panel_Training_2");
        page1.transform.position = page1.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        page2.transform.position = page2.GetComponent<ThemePanelsBehaviour>().TurnOffPosition;
        GameObject.Find("Snowman").GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
    }
}
