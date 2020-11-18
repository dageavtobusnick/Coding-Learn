using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoryBookBehaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = GameObject.Find("UI_Collector").GetComponent<Transform>().position;
        gameObject.transform.position = TurnOffPosition;
    }
}
