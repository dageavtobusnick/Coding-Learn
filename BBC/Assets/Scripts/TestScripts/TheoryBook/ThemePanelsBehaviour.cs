using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePanelsBehaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    private void Start()
    {
        TurnOnPosition = GameObject.Find("Panel_TheoryBook").GetComponent<Transform>().position;
        TurnOffPosition = GameObject.Find("UI_Collector").GetComponent<Transform>().position;
        gameObject.transform.position = TurnOffPosition;
    }
}
