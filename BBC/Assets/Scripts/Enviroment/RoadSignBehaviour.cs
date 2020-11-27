using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadSignBehaviour : MonoBehaviour
{
    GameObject button;

    void Start()
    {
        button = GameObject.Find("TaskButton");
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
