using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadSignBehaviour : MonoBehaviour
{
    private bool wasShowed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!wasShowed)
        {
            var training = GameObject.Find("Panel_Training_1");
            training.transform.position = training.GetComponent<ThemePanelsBehaviour>().TurnOnPosition;
            GameObject.Find("Snowman").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            wasShowed = true;
        }
    }
}
