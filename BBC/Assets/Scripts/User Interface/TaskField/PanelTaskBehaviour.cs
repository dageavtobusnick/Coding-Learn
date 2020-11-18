using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTaskBehaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = GameObject.Find("UI_Collector").transform.position;
        gameObject.transform.position = TurnOffPosition;
    }
}
