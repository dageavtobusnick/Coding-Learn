using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTask2Behaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = new Vector3(-2077, 1019, 0);
        gameObject.transform.position = TurnOffPosition;
    }
}
