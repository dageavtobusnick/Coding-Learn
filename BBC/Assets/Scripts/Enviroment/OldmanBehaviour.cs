using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldmanBehaviour : MonoBehaviour
{
    GameObject button;

    void Start()
    {
        button = GameObject.Find("OldmanButton");
        button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (button != null)
            button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (button != null)
            button.SetActive(false);
    }
}
