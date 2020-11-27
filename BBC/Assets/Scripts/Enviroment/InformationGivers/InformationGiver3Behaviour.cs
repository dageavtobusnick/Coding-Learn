using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationGiver3Behaviour : MonoBehaviour
{
    GameObject button;

    void Start()
    {
        button = GameObject.Find("InformationButton_3");
        button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        button.SetActive(false);
    }
}
