using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskFieldBehaviour : MonoBehaviour
{
    public Vector3 TurnOnPosition;
    public Vector3 TurnOffPosition;

    void Start()
    {
        TurnOnPosition = gameObject.transform.position;
        TurnOffPosition = new Vector3(-358, -725, 0);
        gameObject.transform.position = TurnOffPosition;
    }
}
