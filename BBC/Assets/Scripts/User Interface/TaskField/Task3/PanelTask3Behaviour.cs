using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTask3Behaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = new Vector3(-2080, 1617, 0);
        gameObject.transform.position = TurnOffPosition;
    }
}
