using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangePanelBehaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    private void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = GameObject.Find("UI_Collector").GetComponent<Transform>().position;
        gameObject.transform.position = TurnOffPosition;
    }
}
