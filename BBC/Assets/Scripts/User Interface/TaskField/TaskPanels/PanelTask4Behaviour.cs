using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTask4Behaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = new Vector3(-2079, 2229, 0);
        gameObject.transform.position = TurnOffPosition;
    }
}
