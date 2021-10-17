using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public GameObject CanvasDoorPuzzle;
    public Collider playerCollider;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider == playerCollider)
        {
            CanvasDoorPuzzle.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider == playerCollider)
        {
            CanvasDoorPuzzle.SetActive(false);
        }
    }
}
