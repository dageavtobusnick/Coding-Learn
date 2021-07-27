using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSceneBehaviour : MonoBehaviour
{
    public Camera PreviousCamera;
    public Camera NextCamera;
    public int destinationTriggerNumber;
    public string buttonText;
    [Tooltip("Должна ли мини-карта быть активной")]
    public bool IsMinimapShouldActive;
}
