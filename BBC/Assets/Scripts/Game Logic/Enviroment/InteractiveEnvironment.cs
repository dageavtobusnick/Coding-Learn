using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveEnvironment : MonoBehaviour
{
    public string RequiredItemName;
    public bool HasCodingPuzzle;
    [Space]
    public UnityEvent<GameObject> OnPuzzleSolved;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<InteractiveItemMarker>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<InteractiveItemMarker>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.Player.GetComponent<PlayerBehaviour>().FreezePlayer();
            GameManager.Instance.CurrentInteractiveObject = gameObject;
            UIManager.Instance.Canvas.GetComponentInChildren<InventoryBehaviour>().ShowInventory_SolvePuzzle();
        }
    }
}
