using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBehaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = new Vector3(780, 1750, 0);
        gameObject.transform.position = TurnOffPosition;
    }
}
