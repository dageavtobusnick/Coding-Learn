using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby3Behaviour : MonoBehaviour
{
    private GameObject button;
    private bool isClickAvailable = false;

    void Start()
    {
        button = GameObject.Find("PressToPickUp_Ruby3Button");
        button.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isClickAvailable)
        {
            GameObject.Find("RubyCounter").GetComponent<RubyCounterBehaviour>().rubyCount++;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        button.SetActive(true);
        isClickAvailable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        button.SetActive(false);
        isClickAvailable = false;
    }
}
