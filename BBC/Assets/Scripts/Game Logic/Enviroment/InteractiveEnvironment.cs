using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveEnvironment : MonoBehaviour
{
    public string RequiredItemName;
    public UnityEvent OnPuzzleCalled;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<InteractiveItemMarker>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<InteractiveItemMarker>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GetComponent<InteractiveItemMarker>().enabled)
        {
            GameManager.Instance.CurrentInteractiveEnvironment = gameObject;
            UIManager.Instance.Canvas.GetComponentInChildren<InventoryBehaviour>().ShowInventory_SolvePuzzle();
        }
    }
}
